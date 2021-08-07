using Volo.Abp;

namespace EShopOnAbp.Vips
{
    public class InvalidScoreException : BusinessException
    {
        public InvalidScoreException(int score) 
            : base("VM:00001", message: $"积分不可为负数!{score}")
        {
        }
    }
}