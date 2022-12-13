using UnityEngine;
using System;
using Game.Characters.Enums;
using DI.Kernel.Interfaces;
using DI.Attributes.Register;


namespace Game.Characters.Handlers.Abstract
{
    internal class HandlerBase : MonoBehaviour
    {
        [SerializeField] private TypeAim typeAim;
        private protected Predicate<Transform> check;

#region Mono Behaviour

        private void Start()
        {
            check = SetTypeVerification();
        }

#endregion

        private Predicate<Transform> SetTypeVerification()
        {
            switch (typeAim)
            {
                case TypeAim.Player: return (Transform t) => t.GetComponent<Player>();
                case TypeAim.Hand: return (Transform t) => t.CompareTag("Hand");
                default: throw new ArgumentException("Недопустимый ключ");
            }
        }
    }
}
