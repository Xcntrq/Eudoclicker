using nsPageSelectionButton;

namespace nsPageSelectionButtonR
{
    public class PageSelectionButtonR : PageSelectionButton
    {
        protected override void OnClick()
        {
            _pageNumber.Increase();
        }

        protected override void PageNumber_OnValueChange(int value)
        {
            Switch(value < _pageNumber.MaxValue);
        }
    }
}
