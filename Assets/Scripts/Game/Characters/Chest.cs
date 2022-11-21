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
    [Register]
    internal class Chest : MonoBehaviour, IKernelEntity, IChest
    {
        [SerializeField] private ChestEntityPhysics chestEntity;

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
        
#region MonoBehaviour

        private void Start()
        {
            animator = GetComponent<Animator>();
            selectionAura.SetActive(false);
        }

        private void OnDestroy()
        {
            destroyParticles.transform.parent = null;
            destroyParticles.IsActivated = true;
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
            
            chestEntity.gameObject.SetActive(true);
            
            Instantiate(chestEntity, transform.position, Quaternion.identity, null);
            Debug.Log("Chest instantiate");
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
