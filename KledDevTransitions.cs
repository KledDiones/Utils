using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KledDev.Transitions
{
    public class KledDevTransitions : MonoBehaviour
    {
        /// <summary>
            /// Instância única da classe KledDevTransitions.
            /// </summary>
        static KledDevTransitions instance;

        /// <summary>
            /// Cria uma instância da classe KledDevTransitions se nenhuma existir.
            /// </summary>
        static void CreateInstance()
        {
            GameObject @object = new("KledDevTransitions"); // Cria um GameObject chamado "KledDevTransitions".
            @object.AddComponent<KledDevTransitions>(); // Adiciona o componente KledDevTransitions ao GameObject.
            instance = @object.GetComponent<KledDevTransitions>(); // Obtém a referência ao componente KledDevTransitions.
        }

        /// <summary>
            /// Obtém a instância da classe KledDevTransitions.
            /// </summary>
            /// <returns>A instância da classe KledDevTransitions.</returns>
        static KledDevTransitions GetInstance()
        {
            if (instance == null)
            {
                CreateInstance(); // Cria a instância se ainda não existir.
            }
            return instance;
        }

        /// <summary>
        ///  **Seção Call (Chamada)**  
        ///  Esta seção contém métodos públicos para iniciar efeitos de transição. 
        /// </summary>

        #region Call

        /// <summary>
        /// Aplica um efeito de fade (dissolvimento gradual) a um CanvasGroup.
        /// </summary>
        /// <param name="screen">O CanvasGroup que receberá o efeito de fade.</param>
        /// <param name="startAlpha">O valor alpha inicial do fade (0 é totalmente transparente, 1 é totalmente opaco).</param>
        /// <param name="endAlpha">O valor alpha final do fade (0 é totalmente transparente, 1 é totalmente opaco).</param>
        /// <param name="duration">A duração do efeito de fade em segundos.</param>
        /// <param name="action">Uma ação (delegate) a ser executada após o término do efeito.</param>
        public static void FadeEffect(CanvasGroup screen, float startAlpha, float endAlpha, float duration, Action action)
        {
            GetInstance().StartCoroutine(Fade(screen, startAlpha, endAlpha, duration, action));
        }

        /// <summary>
        /// Aplica um efeito de zoom a um Transform.
        /// </summary>
        /// <param name="screen">O Transform que receberá o efeito de zoom.</param>
        /// <param name="startZoom">O valor de zoom inicial (1 é o tamanho original).</param>
        /// <param name="endZoom">O valor de zoom final (1 é o tamanho original).</param>
        /// <param name="duration">A duração do efeito de zoom em segundos.</param>
        /// <param name="action">Uma ação (delegate) a ser executada após o término do efeito.</param>
        public static void ZoomEffect(Transform screen, float startZoom, float endZoom, float duration, Action action)
        {
            GetInstance().StartCoroutine(Zoom(screen, startZoom, endZoom, duration, action));
        }

        #endregion

        /// <summary>
        ///  **Seção Routines (Coroutines)**  
        ///  Esta seção contém as coroutines utilizadas para executar os efeitos de transição. 
        /// </summary>

        #region Routines

        /// <summary>
        /// Coroutine que realiza o efeito de fade (dissolvimento gradual) em um CanvasGroup.
        /// </summary>
        /// <param name="group">O CanvasGroup que receberá o efeito de fade.</param>
        /// <param name="startAlpha">O valor alpha inicial do fade (0 é totalmente transparente, 1 é totalmente opaco).</param>
        /// <param name="endAlpha">O valor alpha final do fade (0 é totalmente transparente, 1 é totalmente opaco).</param>
        /// <param name="duration">A duração do efeito de fade em segundos.</param>
        /// <param name="action">Uma ação (delegate) a ser executada após o término do efeito.</param>
        /// <returns>A coroutine que está sendo executada.</returns>
        static IEnumerator Fade(CanvasGroup group, float startAlpha, float endAlpha, float duration, Action action)
        {
            float elapsedTime = 0;

            while (elapsedTime < duration)
            {
                group.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            group.alpha = endAlpha; // Garante que o valor final seja alcançado

            if (group.alpha == endAlpha)
            {
                if(action != null)
                {
                    action.Invoke();
                }
            }
        }

        static IEnumerator Zoom(Transform screen, float startZoom, float endZoom, float duration, Action action)
        {
            float elapsedTime = 0;

            while (elapsedTime < duration)
            {
                screen.localScale = Vector3.one*Mathf.Lerp(startZoom, endZoom, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            screen.localScale = Vector3.one * endZoom;

            if (screen.localScale.x == endZoom)
            {
                if (action != null)
                {
                    action.Invoke();
                }
            }
        }

        #endregion


    }
}