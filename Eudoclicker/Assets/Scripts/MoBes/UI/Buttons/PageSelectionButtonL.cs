using nsPageSelectionButton;

namespace nsPageSelectionButtonL
{
    public class PageSelectionButtonL : PageSelectionButton
    {
        protected override void OnClick()
        {
            _pageNumber.Decrease();
        }

        protected override void PageNumber_OnValueChange(int value)
        {
            Switch(value > _pageNumber.MinValue);
        }
    }
}
