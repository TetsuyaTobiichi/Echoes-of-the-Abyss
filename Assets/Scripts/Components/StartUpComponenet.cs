using System.Threading.Tasks;
using Components.Container;
using Constants;
using Mono.Cecil;
using Systems;
using UnityEngine;


namespace Components
{
    public class StartUpComponenet : MonoBehaviour
    {

        async Task Start()
        {
            ObjectsContainer objectsContainer = new();
            objectsContainer.Initialize();

            InputSystem inputSystem = new();
            inputSystem.Initialize();

            objectsContainer.Register<IInputSystem>(inputSystem);
            AddresablesProvider addresablesProvider = new AddresablesProvider();
            Task<GameObject> op = addresablesProvider.LoadAsync<GameObject>(ResourceKeys.Player.PlayerPrefab);
            GameObject result = await op;

            result = Instantiate(result);

            PlayerControll playerControll = result.GetComponent<PlayerControll>();
            playerControll.Initialize(objectsContainer);
        }


        void Update()
        {

        }
    }
}