using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class View : MonoBehaviour
    {
        private int _ID;
        public int ID
        {
            get { return _ID; }
            set { _ID = value;
                UpdateView();
            }
        }

        protected virtual void UpdateView()
        {

        }
    }

}