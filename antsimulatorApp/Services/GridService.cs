using antsimulatorApp.Models;
using Newtonsoft.Json;
using System.Text;


namespace antsimulatorApp.Services
{
    public class GridService
    {
        private static readonly string Uri = "https://localhost:7109";
        private static readonly HttpClient client = new HttpClient(new HttpClientHandler());
        public static int[,] ConvertFlatTo2D(int[] flatArray, int rows, int columns)
        {
            int[,] result = new int[rows, columns];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    result[i, j] = flatArray[i * columns + j];
                }
            }

            return result;
        }
        public async Task<GridModel> UploadGrid(int[,] grid)
        {
            var gridModel = new GridModel { Grid = grid };

            var json = JsonConvert.SerializeObject(gridModel.FlatGrid);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var res = await client.PostAsync($"{Uri}/Grid/UploadGrid", content);

            string flatGridString = await res.Content.ReadAsStringAsync();

            string[] values = flatGridString
            .TrimStart('[')
            .TrimEnd(']')
            .Split(',');

            int[] flatGrid = values.Select(int.Parse).ToArray();

            int rows = 40;
            int columns = 40;

            int[,] originalGrid = ConvertFlatTo2D(flatGrid, rows, columns);
            var model = new GridModel()
            {
                Grid = originalGrid
            };

            return model;
        }

        public async Task<GridModel> GetGrid()
        {
            var res = await client.GetAsync($"{Uri}/Grid/GetGrid");

            if (!res.IsSuccessStatusCode)
            {
                return null;
            }

            string flatGridString = await res.Content.ReadAsStringAsync();

            string[] values = flatGridString
            .TrimStart('[')
            .TrimEnd(']')
            .Split(',');

            int[] flatGrid = values.Select(int.Parse).ToArray();

            int rows = 40;
            int columns = 40;

            int[,] originalGrid = ConvertFlatTo2D(flatGrid, rows, columns);

            var model = new GridModel()
            {
                Grid = originalGrid
            };

            return model;
        }
    }
}
