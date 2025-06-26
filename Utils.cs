using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Common.Utils
{
    public class Utils : MonoBehaviour
    {
        /// <summary>
        /// Inst�ncia �nica da classe Utils.
        /// </summary>
        static Utils instance;

        /// <summary>
        /// Cria uma inst�ncia da classe Utils se nenhuma existir.
        /// </summary>
        static void CreateInstance()
        {
            GameObject @object = new("Utils"); // Cria um GameObject chamado "Utils".
            @object.AddComponent<Utils>(); // Adiciona o componente Utils ao GameObject.
            instance = @object.GetComponent<Utils>(); // Obt�m a refer�ncia ao componente Utils.
        }

        /// <summary>
        /// Obt�m a inst�ncia da classe Utils.
        /// </summary>
        /// <returns>A inst�ncia da classe Utils.</returns>
        static Utils GetInstance()
        {
            if (instance == null)
            {
                CreateInstance(); // Cria a inst�ncia se ainda n�o existir.
            }
            return instance;
        }

        /// <summary>
        /// Faz uma pausa de x segundos e depois executa a a��o playAction.
        /// </summary>
        /// <param name="xSeconds">O n�mero de segundos para esperar.</param>
        /// <param name="playAction">A a��o a ser executada ap�s a espera.</param>
        public static void WaitXSecondsForPlayAction(float xSeconds, Action playAction)
        {
            if (xSeconds == 0) return;
            GetInstance().StartCoroutine(WaitXSecondsForPlayActionCoroutine(xSeconds, playAction)); // Inicia a coroutine que espera e executa a a��o.
        }

        /// <summary>
        /// Remove acentos de uma string.
        /// </summary>
        /// <param name="text">A string com acentos.</param>
        /// <returns>A string sem acentos.</returns>
        public static string RemoveAccents(string text)
        {
            string decomposed = text.Normalize(NormalizationForm.FormD); // Normaliza a string para remover acentos.
            Regex regex = new Regex(@"\p{M}"); // Cria uma express�o regular para identificar caracteres de marca��o de acento.
            string noAccents = regex.Replace(decomposed, string.Empty); // Substitui os caracteres de marca��o de acento por string vazia.
            return noAccents;
        }

        /// <summary>
        /// Destroi todos os filhos de um Transform.
        /// </summary>
        /// <param name="content">O Transform que cont�m os filhos a serem destru�dos.</param>
        public static void ClearChilds(Transform content)
        {
            for (int i = 0; i < content.childCount; i++)
            {
                Destroy(content.GetChild(i).gameObject); // Destroi cada GameObject filho do Transform.
            }
        }

        /// <summary>
        /// Preenche um Transform com GameObjects instanciados a partir de um prefab.
        /// </summary>
        /// <param name="objIDList">Uma lista de IDs de objetos.</param>
        /// <param name="content">O Transform que receber� os GameObjects instanciados.</param>
        /// <param name="viewObject">O prefab a ser usado para instanciar os GameObjects.</param>
        public static List<View> FillContent(List<string> objIDList, Transform content, GameObject viewObject, bool clearContent = true)
        {
            List<View> views = new List<View>();

            if (clearContent)
                ClearChilds(content); // Limpa o conte�do do Transform.

            for (int i = 0; i < objIDList.Count; i++)
            {
                GameObject view = Instantiate(viewObject, content); // Instancia o prefab no Transform.
                view.GetComponent<View>().Initialize(objIDList[i]); // Define a propriedade ID do componente View com o ID da lista.
                views.Add(view.GetComponent<View>()); // Adiciona o componente View � lista de views.
            }

            return views; // Retorna a lista de views.
        }

        static IEnumerator WaitXSecondsForPlayActionCoroutine(float xSeconds, Action playAction)
        {
            yield return new WaitForSeconds(xSeconds); // Aguarda o n�mero de segundos especificado.
            playAction.Invoke(); // Executa a a��o playAction.
        }
    }
}