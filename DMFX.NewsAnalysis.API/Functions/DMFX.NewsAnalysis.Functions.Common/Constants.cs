namespace DMFX.NewsAnalysis.Functions.Common
{
    public class Constants
    {
        public static readonly string ENV_DAL_TYPE = "ServiceConfig__DALType";

        public static readonly string ENV_SQL_CONNECTION_STRING = "ServiceConfig__DalInitParams__ConnectionString";

        public static readonly string ENV_STORAGE_CONNECTION_STRING = "ServiceConfig__StorageInitParams__StorageConnectionString";

        public static readonly string ENV_JWT_SECRET = "ServiceConfig__AppSettings__Secret";

        public static readonly string ENV_SESSION_TIMEOUT = "ServiceConfig__AppSettings__SessionTimeout";

        public static readonly string ENV_QUEUE_EXTRACT_REQUESTS = "ServiceConfig__CrawlerQueueExtractRequests";

        public static readonly string ENV_CRAWLER_TIME_LOOKBACK_MINS = "ServiceConfig__CrawlerTimeLookbackMins";

        public static readonly string ENV_CRAWLER_SKIP_EXISTING = "ServiceConfig__CrawlerSkipExisting";
    }
}