using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using AppStudio.DataProviders;
using AppStudio.DataProviders.Core;
using AppStudio.DataProviders.RestApi;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Холодильник
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RcpPage : Section<Schema1Schema>

    {
        String enabled;
        public RcpPage()
        {
            this.InitializeComponent();
            _dataProvider = new RestApiDataProvider();


        }

        private RestApiDataProvider _dataProvider;

        

        public override async Task<IEnumerable<Schema1Schema>> GetDataAsync(SchemaBase connectedItem = null)
        {
            var config = new RestApiDataConfig
            {
                Url = new Uri("https://api.vk.com/method/wall.get?owner_id=-44235988&count=1"),
                PaginationConfig = new PageNumberPagination("offset", false, "")
            };

            return await _dataProvider.LoadDataAsync(config, MaxRecords, new Schema1SchemaParser());
        }

        public override async Task<IEnumerable<Schema1Schema>> GetNextPageAsync()
        {
            return await _dataProvider.LoadMoreDataAsync<Schema1Schema>();
        }

        public override bool HasMorePages
        {
            get
            {
                return _dataProvider.HasMoreItems;
            }
        }

        public override ListPageConfig<Schema1Schema> ListPage
        {
            get
            {
                return new ListPageConfig<Schema1Schema>
                {
                    Title = "Новости",

                    Page = typeof(Pages.Section1ListPage),

                    LayoutBindings = (viewModel, item) =>
                    {
                        viewModel.SubTitle = item.text.ToSafeString();
                        viewModel.ImageUrl = ItemViewModel.LoadSafeUrl(item.src.ToSafeString());
                    },
                    DetailNavigation = (item) =>
                    {
                        return NavInfo.FromPage<Pages.Section1DetailPage>(true);
                    }
                };
            }
        }

        public override DetailPageConfig<Schema1Schema> DetailPage
        {
            get
            {
                var bindings = new List<Action<ItemViewModel, Schema1Schema>>();
                bindings.Add((viewModel, item) =>
                {
                    viewModel.PageTitle = "Post";
                    viewModel.Title = "";
                    viewModel.Description = item.text.ToSafeString();
                    viewModel.ImageUrl = ItemViewModel.LoadSafeUrl(item.src.ToSafeString());
                    viewModel.Content = null;
                });

                var actions = new List<ActionConfig<Schema1Schema>>
                {
                };

                return new DetailPageConfig<Schema1Schema>
                {
                    Title = "Новости",
                    LayoutBindings = bindings,
                    Actions = actions
                };
            }
        }

        
    }
    public class Schema1Schema : SchemaBase
    {

        public string text { get; set; }

        public string src { get; set; }
    }

}
