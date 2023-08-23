namespace Ecreo.Essentials.UmbracoTemplate.Controllers.RenderControllers;

public class SearchResultsPageController : RenderController<ContentModels.SearchResultsPage>
{
    private readonly ISearchService _searchService;
    private readonly FullTextSearchOptions _options;

    public SearchResultsPageController(
        ISearchService searchService,
        IOptions<FullTextSearchOptions> options,
        ILogger<RenderController> logger, ICompositeViewEngine compositeViewEngine, IUmbracoContextAccessor umbracoContextAccessor) : base(logger, compositeViewEngine, umbracoContextAccessor)
    {
        _searchService = searchService;
        _options = options.Value;
    }
    public override IActionResult Index()
    {
        CurrentPage.FullTextSearchResult = null;

        if (Request.Query.ContainsKey("q"))
        {
            var currentPage = PaginationHelper.GetCurrentPageFromQueryString(Request, totalPages: int.MaxValue);

            var search = new Search(Request.Query["q"].ToString())
                .EnableHighlighting()
                .AddTitleProperty("metaTitle")
                .AddTitleProperty("nodeName")
                .AddSummaryProperty("metaDescription")
                .AddSummaryProperty(_options.FullTextContentField)
                .SetSummaryLength(160)
                .SetPageLength(10)
                .SetCulture(CurrentPage.GetCultureFromDomains()?.ToLower());

            CurrentPage.FullTextSearchResult = _searchService.Search(search, currentPage);
        }

        return CurrentTemplate(CurrentPage);
    }
}
