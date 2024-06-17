using FahasaStoreAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace FahasaStoreAPI.Services
{
    public class ItemBaseCF
    {
        private readonly FahasaStoreDBContext _context;

        public ItemBaseCF(FahasaStoreDBContext context)
        {
            _context = context;
        }

        // Tạo ma trận người dùng - sản phẩm từ dữ liệu giỏ hàng
        public Dictionary<string, Dictionary<int, float>> CreateUserItemMatrix()
        {
            var userItemMatrix = new Dictionary<string, Dictionary<int, float>>();

            // Lấy danh sách các mục trong giỏ hàng từ cơ sở dữ liệu
            var cartItems = _context.CartItems.Include(ci => ci.Cart);

            // Duyệt qua từng mục trong giỏ hàng để tạo ma trận người dùng - sản phẩm
            foreach (var cartItem in cartItems)
            {
                if (!userItemMatrix.ContainsKey(cartItem.Cart.UserId))
                {
                    userItemMatrix[cartItem.Cart.UserId] = new Dictionary<int, float>();
                }
                userItemMatrix[cartItem.Cart.UserId][cartItem.BookId] = cartItem.Quantity;
            }

            return userItemMatrix;
        }

        // Tính toán độ tương đồng Cosine Similarity giữa hai vector
        public double CosineSimilarity(Dictionary<int, float> vectorA, Dictionary<int, float> vectorB)
        {
            var commonKeys = vectorA.Keys.Intersect(vectorB.Keys); // Lấy các key chung
            var dotProduct = commonKeys.Sum(k => vectorA[k] * vectorB[k]); // Tính tích vô hướng

            // Tính độ lớn của vector A
            var magnitudeA = Math.Sqrt(vectorA.Values.Sum(v => v * v));
            // Tính độ lớn của vector B
            var magnitudeB = Math.Sqrt(vectorB.Values.Sum(v => v * v));

            if (magnitudeA == 0 || magnitudeB == 0)
                return 0;

            // Tính cosine similarity
            return dotProduct / (magnitudeA * magnitudeB);
        }

        // Tạo ma trận độ tương đồng sản phẩm
        public Dictionary<int, Dictionary<int, double>> CalculateItemSimilarityMatrix(Dictionary<string, Dictionary<int, float>> userItemMatrix)
        {
            var itemSimilarityMatrix = new Dictionary<int, Dictionary<int, double>>();

            // Lấy danh sách tất cả sách từ cơ sở dữ liệu
            var allBooks = _context.Books.ToList();

            // Duyệt qua từng sách để tính toán ma trận độ tương đồng sản phẩm
            foreach (var book1 in allBooks)
            {
                itemSimilarityMatrix[book1.Id] = new Dictionary<int, double>();
                foreach (var book2 in allBooks)
                {
                    if (book1.Id == book2.Id) continue;

                    // Lấy vector của sách 1 và sách 2 từ ma trận người dùng - sản phẩm
                    var vectorA = userItemMatrix
                        .Where(x => x.Value.ContainsKey(book1.Id))
                        .ToDictionary(x => x.Key, x => x.Value[book1.Id]);

                    var vectorB = userItemMatrix
                        .Where(x => x.Value.ContainsKey(book2.Id))
                        .ToDictionary(x => x.Key, x => x.Value[book2.Id]);

                    // Tính toán độ tương đồng cosine similarity
                    // itemSimilarityMatrix[book1.Id][book2.Id] = CosineSimilarity(vectorA, vectorB);
                }
            }

            return itemSimilarityMatrix;
        }
    }

}
