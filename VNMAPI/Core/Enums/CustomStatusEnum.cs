using Microsoft.OpenApi.Attributes;

namespace Core.Enums
{
    public enum CustomStatusEnum
    {
        [Display("True")]
        SUCCESS,

        [Display("False")]
        ERROR
    }
}
