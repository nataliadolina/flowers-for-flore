using UnityEngine;
using DI.Attributes.Register;
using DI.Attributes.Construct;
using DI.Kernel.Interfaces;
using Game.Characters.Interfaces;
using Game.Characters.Abstract;
using Game.Characters.Effects;
using DI.Kernel.Enums;

namespace Game.Characters
{
    [Register(typeof(IChest))]
    internal class Chest : MonoBehaviour, IKernelEntity, IChest
    {
        [SerializeField] private Particles appearParticles;
        [SerializeField] private Particles destroyParticles;

        [SerializeField] private GameObject selectionAura;

        private Animator animator;
        private bool _isSelected = false;
        private bool _isVisible = false;

        public bool IsSelected {
            get => _isSelected;
            set { 
                selectionAura.SetActive(value);
                _isSelected = value;
            }
        }

        public bool IsVisible
        {
            get => _isVisible;
            set
            {
                _isVisible = value;
                appearParticles.IsActivated = value;
            }
        }

#region ITransform

        public Transform Transform { get; private set; }

#endregion

#region MonoBehaviour

        private void Awake()
        {
            Transform = transform;
        }

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        protected void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Player>())
            {
                
            }
        }

        protected void OnTriggerStay(Collider other)
        {
            if (other.GetComponent<Player>())
            {
                
            }
        }

        protected void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<Player>())
            {
                
            }
        }

#endregion

        public void Open()
        {
            animator.SetTrigger("open");

            _chestEntitySetActive.IsActive = true;
            
            Debug.Log("Chest instantiate");
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }

#region Kernel Entity

        private ISetActive _chestEntitySetActive;

        [ConstructMethod]
        private void OnConstruct(IKernel kernel)
        {
            _chestEntitySetActive = kernel.GetInjection<IChestEntity>();
        }

#endregion

    }
}
