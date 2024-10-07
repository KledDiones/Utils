using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KledDev.Transitions
{
    public class KledDevTransitions : MonoBehaviour
    {
        /// <summary>
        ����/// Inst�ncia �nica da classe KledDevTransitions.
        ����/// </summary>
        static KledDevTransitions instance;

        /// <summary>
        ����/// Cria uma inst�ncia da classe KledDevTransitions se nenhuma existir.
        ����/// </summary>
        static void CreateInstance()
        {
            GameObject @object = new("KledDevTransitions"); // Cria um GameObject chamado "KledDevTransitions".
            @object.AddComponent<KledDevTransitions>(); // Adiciona o componente KledDevTransitions ao GameObject.
            instance = @object.GetComponent<KledDevTransitions>(); // Obt�m a refer�ncia ao componente KledDevTransitions.
        }

        /// <summary>
        ����/// Obt�m a inst�ncia da classe KledDevTransitions.
        ����/// </summary>
        ����/// <returns>A inst�ncia da classe KledDevTransitions.</returns>
        static KledDevTransitions GetInstance()
        {
            if (instance == null)
            {
                CreateInstance(); // Cria a inst�ncia se ainda n�o existir.
            }
            return instance;
        }

        /// <summary>
        ///  **Se��o Call (Chamada)**  
        ///  Esta se��o cont�m m�todos p�blicos para iniciar efeitos de transi��o. 
        /// </summary>

        #region Call

        /// <summary>
        /// Aplica um efeito de fade (dissolvimento gradual) a um CanvasGroup.
        /// </summary>
        /// <param name="screen">O CanvasGroup que receber� o efeito de fade.</param>
        /// <param name="startAlpha">O valor alpha inicial do fade (0 � totalmente transparente, 1 � totalmente opaco).</param>
        /// <param name="endAlpha">O valor alpha final do fade (0 � totalmente transparente, 1 � totalmente opaco).</param>
        /// <param name="duration">A dura��o do efeito de fade em segundos.</param>
        /// <param name="action">Uma a��o (delegate) a ser executada ap�s o t�rmino do efeito.</param>
        public static void FadeEffect(CanvasGroup screen, float startAlpha, float endAlpha, float duration, Action action)
        {
            GetInstance().StartCoroutine(Fade(screen, startAlpha, endAlpha, duration, action));
        }

        /// <summary>
        /// Aplica um efeito de zoom a um Transform.
        /// </summary>
        /// <param name="screen">O Transform que receber� o efeito de zoom.</param>
        /// <param name="startZoom">O valor de zoom inicial (1 � o tamanho original).</param>
        /// <param name="endZoom">O valor de zoom final (1 � o tamanho original).</param>
        /// <param name="duration">A dura��o do efeito de zoom em segundos.</param>
        /// <param name="action">Uma a��o (delegate) a ser executada ap�s o t�rmino do efeito.</param>
        public static void ZoomEffect(Transform screen, float startZoom, float endZoom, float duration, Action action)
        {
            GetInstance().StartCoroutine(Zoom(screen, startZoom, endZoom, duration, action));
        }

        #endregion

        /// <summary>
        ///  **Se��o Routines (Coroutines)**  
        ///  Esta se��o cont�m as coroutines utilizadas para executar os efeitos de transi��o. 
        /// </summary>

        #region Routines

        /// <summary>
        /// Coroutine que realiza o efeito de fade (dissolvimento gradual) em um CanvasGroup.
        /// </summary>
        /// <param name="group">O CanvasGroup que receber� o efeito de fade.</param>
        /// <param name="startAlpha">O valor alpha inicial do fade (0 � totalmente transparente, 1 � totalmente opaco).</param>
        /// <param name="endAlpha">O valor alpha final do fade (0 � totalmente transparente, 1 � totalmente opaco).</param>
        /// <param name="duration">A dura��o do efeito de fade em segundos.</param>
        /// <param name="action">Uma a��o (delegate) a ser executada ap�s o t�rmino do efeito.</param>
        /// <returns>A coroutine que est� sendo executada.</returns>
        static IEnumerator Fade(CanvasGroup group, float startAlpha, float endAlpha, float duration, Action action)
        {
            float elapsedTime = 0;

            while (elapsedTime < duration)
            {
                group.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            group.alpha = endAlpha; // Garante que o valor final seja alcan�ado

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