using Terraria.UI;

namespace AAMod.Common.Systems.UI
{
    public struct UIEntry
    {
        public UIState State;
        public UserInterface UserInterface;

        public int InsertionIndex;

        public UIEntry(UIState state, UserInterface userInterface, int insertionIndex)
        {
            State = state;
            UserInterface = userInterface;
            InsertionIndex = insertionIndex;
        }
    }
}