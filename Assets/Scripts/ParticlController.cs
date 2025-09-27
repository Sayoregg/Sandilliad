using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    internal class ParticlController : MonoBehaviour
    {
        private void Start()
        {
            this.GetComponent<ParticleSystem>().Stop();
        }
        private void Update()
        {
            if (ValueManager.IsSucking)
            {
                this.GetComponent<ParticleSystem>().Play();
              
            }
            else { this.GetComponent<ParticleSystem>().Stop(); }

        }
    }
}
