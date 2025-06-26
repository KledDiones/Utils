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
        /// Instância única da classe Utils.
        /// </summary>
        static Utils instance;

        /// <summary>
        /// Cria uma instância da classe Utils se nenhuma existir.
        /// </summary>
        static void CreateInstance()
        {
            GameObject @object = new("Utils"); // Cria um GameObject chamado "Utils".
            @object.AddComponent<Utils>(); // Adiciona o componente Utils ao GameObject.
            instance = @object.GetComponent<Utils>(); // Obtém a referência ao componente Utils.
        }

        /// <summary>
        /// Obtém a instância da classe Utils.
        /// </summary>
        /// <returns>A instância da classe Utils.</returns>
        static Utils GetInstance()
        {
            if (instance == null)
            {
                CreateInstance(); // Cria a instância se ainda não existir.
            }
            return instance;
        }

        /// <summary>
        /// Faz uma pausa de x segundos e depois executa a ação playAction.
        /// </summary>
        /// <param name="xSeconds">O número de segundos para esperar.</param>
        /// <param name="playAction">A ação a ser executada após a espera.</param>
        public static void WaitXSecondsForPlayAction(float xSeconds, Action playAction)
        {
            if (xSeconds == 0) return;
            GetInstance().StartCoroutine(WaitXSecondsForPlayActionCoroutine(xSeconds, playAction)); // Inicia a coroutine que espera e executa a ação.
        }

        /// <summary>
        /// Remove acentos de uma string.
        /// </summary>
        /// <param name="text">A string com acentos.</param>
        /// <returns>A string sem acentos.</returns>
        public static string RemoveAccents(string text)
        {
            string decomposed = text.Normalize(NormalizationForm.FormD); // Normaliza a string para remover acentos.
            Regex regex = new Regex(@"\p{M}"); // Cria uma expressão regular para identificar caracteres de marcação de acento.
            string noAccents = regex.Replace(decomposed, string.Empty); // Substitui os caracteres de marcação de acento por string vazia.
            return noAccents;
        }

        /// <summary>
        /// Destroi todos os filhos de um Transform.
        /// </summary>
        /// <param name="content">O Transform que contém os filhos a serem destruídos.</param>
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
        /// <param name="content">O Transform que receberá os GameObjects instanciados.</param>
        /// <param name="viewObject">O prefab a ser usado para instanciar os GameObjects.</param>
        public static List<View> FillContent(List<string> objIDList, Transform content, GameObject viewObject, bool clearContent = true)
        {
            List<View> views = new List<View>();

            if (clearContent)
                ClearChilds(content); // Limpa o conteúdo do Transform.

            for (int i = 0; i < objIDList.Count; i++)
            {
                GameObject view = Instantiate(viewObject, content); // Instancia o prefab no Transform.
                view.GetComponent<View>().Initialize(objIDList[i]); // Define a propriedade ID do componente View com o ID da lista.
                views.Add(view.GetComponent<View>()); // Adiciona o componente View à lista de views.
            }

            return views; // Retorna a lista de views.
        }

        static IEnumerator WaitXSecondsForPlayActionCoroutine(float xSeconds, Action playAction)
        {
            yield return new WaitForSeconds(xSeconds); // Aguarda o número de segundos especificado.
            playAction.Invoke(); // Executa a ação playAction.
        }
    }
}