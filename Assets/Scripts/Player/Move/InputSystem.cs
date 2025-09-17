using Systems;

public class InputSystem : IInputSystem
{
    public InputSystem_Actions Input => _input;
    private InputSystem_Actions _input;

    public void Initialize()
    {
        _input = new InputSystem_Actions();
        _input.Enable();
    }

    public void EnableControll()
    {
        _input.Enable();
    }

    public void DisableControll()
    {
        _input.Disable();
    }
}