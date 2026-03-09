namespace InstagramInfrastructure
{
    public static class InstagramRoutesConstant
    {
        public const string INSTAGRAM_ACCOUNT_ID = "me?access_token={0}";
        public const string INSTAGRAM_ACCOUNT = "me/media?fields=id,caption,media_url,permalink&access_token={0}";

        public const string INSTAGRAM_PROFILE_BASIC_INFO = "{0}?fields=id,username,name,biography,profile_picture_url,website&access_token={1}";
        public const string INSTAGRAM_PROFILE_STATS_INFO = "{0}?fields=followers_count,follows_count,media_count&access_token={1}";
        public const string INSTAGRAM_PROFILE_BUSINESS_INFO = "{0}?fields=account_type,business_category_name,category,contact_phone_number,email,address_street,city_name,zip,contact_phone_number&access_token={1}";

        public const string INSTAGRAM_POSTS = "{0}/media?fields=id,caption,media_url,media_type,permalink&access_token={1}";
        public const string INSTAGRAM_POST_BY_ID = "{0}?fields=id,caption,media_url,like_count,comments_count&access_token={1}";
        public const string INSTAGRAM_POST_UPLOAD = "{0}/media";
        public const string INSTAGRAM_POST_PUBLISH = "me/media_publish";
        public const string INSTAGRAM_POST_COMMENT = "{0}/comments";

        public const string INSTAGRAM_DATA_INSIGHTS = "{0}/insights&access_token={1}";
    }
}
