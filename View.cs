using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class View : MonoBehaviour
    {
        private string _ID;
        protected string ID
        {
            get { return _ID; }
            set { _ID = value;
                UpdateView();
            }
        }

        public virtual void Initialize(string id)
        {
            ID = id;
        }

        protected virtual void UpdateView() { }
    }

}