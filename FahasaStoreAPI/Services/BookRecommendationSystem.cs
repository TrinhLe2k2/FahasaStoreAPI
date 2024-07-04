using FahasaStoreAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace FahasaStoreAPI.Services
{
    public interface IBookRecommendationSystem 
    {
        IEnumerable<int> FindSimilarBooks(Book book, int take);
        IEnumerable<int> FindSimilarBooksBasedOnCart(IEnumerable<Book> cart, int take, string aggregationMethod = "average");
    }

    public class BookRecommendationSystem : IBookRecommendationSystem
    {
        private readonly FahasaStoreDBContext _context;
        private readonly List<Book> _books;
        private readonly Dictionary<int, double[]> _bookFeatureVectors;

        // Tạo một mảng các ký tự đặc biệt bạn muốn loại bỏ
        private char[] punctuationChars = { '~', '`', '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '-', '_', '=', '+', '[', '{', ']', '}', '\\', '|', ';', ':', '\'', '"', '<', ',', '>', '.', '/', '?' };
        private string[] stopWords = {
                "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín", "mười",
                "và", "hoặc", "của", "trong", "ở", "tới", "đến", "cho", "về", "với", "cùng", "là",
                "được", "từ", "đi", "điều", "này", "đó"
            };

        public BookRecommendationSystem()
        {
            _context = new FahasaStoreDBContext();
            _books = _context.Books
                .Include(b => b.Author)
                .Include(b => b.CoverType)
                .Include(b => b.Subcategory)
                    .ThenInclude(sc => sc.Category)
                .ToList();
            _bookFeatureVectors = _books.ToDictionary(book => book.Id, book => FeatureVector(book)[book.Id]);
        }

        // Tiền xử lý văn bản
        private string PreprocessText(string text)
        {
            // Loại bỏ các dấu câu
            foreach (char punctuationChar in punctuationChars)
            {
                text = text.Replace(punctuationChar, ' ');
            }
            // Chuyển đổi văn bản về chữ thường
            text = text.ToLower();
            // Tách các từ và loại bỏ các từ dừng
            var words = text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                            .Where(word => !stopWords.Contains(word));
                            //.Distinct(); // Loại bỏ các từ trùng lặp
            // Gộp các từ lại thành một chuỗi mới
            string processedText = string.Join(" ", words);
            return processedText;
        }

        // Tính toán giá trị Term Frequency (tần suất xuất hiện của từ) cho một từ trong một văn bản.
        private double ComputeTF(string term, string[] words)
        {
            int termFrequency = words.Count(w => w.Equals(term, StringComparison.OrdinalIgnoreCase));

            // Tính toán và trả về giá trị Term Frequency
            var tf = (double)termFrequency / words.Length;
            return Math.Round(tf, 4);
        }

        // Tính toán giá trị Inverse Document Frequency (tần suất nghịch đảo của từ) cho một từ.
        private double ComputeIDF(string term)
        {
            // Đếm số lượng văn bản chứa từ cần tính IDF
            int documentFrequency = _books.Count(book => PreprocessText(book.Description).Contains(term));
            if (documentFrequency == 0)
            {
                return 0.0;
            }
            // Tính toán và trả về giá trị IDF
            var idf = Math.Log((double)_books.Count / (documentFrequency));
            return Math.Round(idf, 4);
        }

        // Tính toán giá trị TF-IDF cho một từ trong một văn bản.
        private double ComputeTFIDF(string term, string[] words)
        {
            double tf = ComputeTF(term, words);
            double idf = ComputeIDF(term);
            double tfidf = tf * idf;
            return Math.Round(tfidf, 4);
        }

        // Chuẩn hóa giá trị số trong khoảng [0,1]
        private double Normalize(double value, double minValue, double maxValue)
        {
            return (value - minValue) / (maxValue - minValue);
        }

        // Mã hóa one-hot encoding cho thuộc tính phân loại
        private double[] OneHotEncode(int value, int distinctCount)
        {
            var encoding = new double[distinctCount];
            encoding[value] = 1.0;
            return encoding;
        }
        private Dictionary<int, double[]> FeatureVector(Book book)
        {
            var combinedString = $"{book.Name} {book.Author.Name} {book.Subcategory.Name} {book.Subcategory.Category.Name} {book.CoverType.TypeName}";
            var words = PreprocessText(combinedString).Split(' ');
            var featureVector = new double[words.Length];

            for (int i = 0; i < words.Length; i++)
            {
                var item = ComputeTFIDF(words[i], words);
                featureVector[i] = item;
            }

            var result = new Dictionary<int, double[]>();
            result[book.Id] = featureVector;
            return result;
        }

        // Tạo vector đặc trưng cho book
        private Dictionary<int, double[]> FeatureVector2(Book book)
        {
            var words = PreprocessText(book.Description).Split(' ');
            var featureVector = new double[words.Length];

            for (int i = 0; i < words.Length; i++)
            {
                var item = ComputeTFIDF(words[i], words);
                featureVector[i] = item;
            }

            var result = new Dictionary<int, double[]>();
            result[book.Id] = featureVector;
            return result;
        }

        // Tính tích vô hướng của hai vector.
        private double DotProduct(double[] vectorA, double[] vectorB)
        {
            return vectorA.Zip(vectorB, (a, b) => a * b).Sum();
        }

        // Tính toán độ dài (norm) của một vector.
        private double Norm(double[] vector)
        {
            return Math.Sqrt(vector.Sum(x => x * x));
        }

        // Tính cosine similarity
        private double ComputeCosineSimilarity(double[] vectorA, double[] vectorB)
        {
            // Điều chỉnh độ dài của vector để chúng có cùng độ dài
            AdjustVectors(ref vectorA, ref vectorB);

            // Tính toán cosine similarity
            var dotProduct = DotProduct(vectorA, vectorB);
            var normA = Norm(vectorA);
            var normB = Norm(vectorB);

            // Bảo đảm không chia cho 0
            if (normA == 0 || normB == 0)
            {
                return 0.0;
            }

            var cosineSimilarity = dotProduct / (normA * normB);
            return cosineSimilarity;
        }

        private void AdjustVectors(ref double[] vectorA, ref double[] vectorB)
        {
            // Lấy độ dài của cả hai vector
            int lengthA = vectorA.Length;
            int lengthB = vectorB.Length;

            // Nếu độ dài không bằng nhau, điều chỉnh độ dài
            if (lengthA != lengthB)
            {
                // Độ dài lớn hơn làm chuẩn
                int targetLength = Math.Max(lengthA, lengthB);

                // Điều chỉnh vector A
                if (lengthA < targetLength)
                {
                    Array.Resize(ref vectorA, targetLength);
                }

                // Điều chỉnh vector B
                if (lengthB < targetLength)
                {
                    Array.Resize(ref vectorB, targetLength);
                }
            }
        }

        // Content base
        public IEnumerable<int> FindSimilarBooks(Book book, int take)
        {
            var vectorA = FeatureVector(book);
            var vectorB = vectorA;
            Dictionary<int, double> similarBooksDictionary = new Dictionary<int, double>();

            foreach (var otherBook in _books)
            {
                vectorB = FeatureVector(otherBook);
                var computeCosine = ComputeCosineSimilarity(vectorA[book.Id], vectorB[otherBook.Id]);
                similarBooksDictionary[otherBook.Id] = computeCosine;
            }
            
            var similarBooks = similarBooksDictionary
                .OrderByDescending(e => e.Value)
                .Select(e => e.Key)
                .Where(e => e != book.Id)
                .Take(take);
            var result = similarBooks.ToList();
            return result;
        }

        // ------------------------------------------------------------------ From Cart

        // Tạo vector đặc trưng tổng hợp cho giỏ hàng
        private double[] AggregateFeatureVectors(IEnumerable<double[]> vectors)
        {
            if (!vectors.Any()) return new double[0];
            int length = vectors.Sum(vector => vector.Length);
            double[] aggregateVector = new double[length];

            foreach (var vector in vectors)
            {
                for (int i = 0; i < vector.Length; i++)
                {
                    aggregateVector[i] += vector[i];
                }
            }

            return aggregateVector;
        }

        // Trung bình vector đặc trưng
        private double[] AverageFeatureVectors(IEnumerable<double[]> vectors)
        {
            if (!vectors.Any()) return new double[0];
            int length = vectors.Sum(vector => vector.Length);
            double[] averageVector = new double[length];
            int count = vectors.Count();

            foreach (var vector in vectors)
            {
                for (int i = 0; i < vector.Length; i++)
                {
                    averageVector[i] += vector[i];
                }
            }

            for (int i = 0; i < length; i++)
            {
                averageVector[i] /= count;
            }

            return averageVector;
        }

        // Max pooling
        private double[] MaxPoolingFeatureVectors(IEnumerable<double[]> vectors)
        {
            if (!vectors.Any()) return new double[0];
            int length = vectors.Sum(vector => vector.Length);
            double[] maxVector = new double[length];

            foreach (var vector in vectors)
            {
                for (int i = 0; i < vector.Length; i++)
                {
                    if (vector[i] > maxVector[i])
                    {
                        maxVector[i] = vector[i];
                    }
                }
            }

            return maxVector;
        }

        // Min pooling
        private double[] MinPoolingFeatureVectors(IEnumerable<double[]> vectors)
        {
            if (!vectors.Any()) return new double[0];
            int length = vectors.Sum(vector => vector.Length);
            double[] minVector = new double[length];

            // Khởi tạo giá trị ban đầu của minVector thành giá trị rất lớn
            for (int i = 0; i < length; i++)
            {
                minVector[i] = double.MaxValue;
            }

            foreach (var vector in vectors)
            {
                for (int i = 0; i < vector.Length; i++)
                {
                    if (vector[i] < minVector[i])
                    {
                        minVector[i] = vector[i];
                    }
                }
            }

            return minVector;
        }

        // item-based recommendation
        public IEnumerable<int> FindSimilarBooksBasedOnCart(IEnumerable<Book> cart, int take, string aggregationMethod = "average")
        {
            if (!cart.Any())
            {
                return _books
                    .OrderByDescending(book => book.CreatedAt)
                    .Take(take)
                    .Select(book => book.Id)
                    .ToList();
            }

            var cartVectors = cart.Select(book => _bookFeatureVectors[book.Id]);
            double[] aggregateVector;

            switch (aggregationMethod.ToLower())
            {
                case "average":
                    aggregateVector = AverageFeatureVectors(cartVectors);
                    break;
                case "max":
                    aggregateVector = MaxPoolingFeatureVectors(cartVectors);
                    break;
                case "min":
                    aggregateVector = MinPoolingFeatureVectors(cartVectors);
                    break;
                default:
                    aggregateVector = AggregateFeatureVectors(cartVectors);
                    break;
            }

            HashSet<int> cartBookIds = new HashSet<int>(cart.Select(book => book.Id));
            Dictionary<int, double> similarBooksDictionary = new Dictionary<int, double>();

            foreach (var otherBook in _books)
            {
                if (cartBookIds.Contains(otherBook.Id))
                {
                    continue; 
                }
                var otherBookVector = _bookFeatureVectors[otherBook.Id];
                var similarity = ComputeCosineSimilarity(aggregateVector, otherBookVector);
                similarBooksDictionary[otherBook.Id] = similarity;
            }

            var similarBooks = similarBooksDictionary
                .OrderByDescending(e => e.Value)
                .Select(e => e.Key)
                .Take(take);

            return similarBooks.ToList();
        }
    }
}

