using NS_Input;

public interface ISwipeable : ITappable
{
    // Same list of ITappable.

    // Swipe is a vector from tap to tapRelease. The direction shows the swipe type: up, down, left, right...
    public void ExecuteTapRelease(SwipeDirection swipeDirection);
}
