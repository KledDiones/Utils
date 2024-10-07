using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

namespace KledDev.Utils
{
    public class KledDevUtils : MonoBehaviour
    {
        /// <summary>
        ����/// Inst�ncia �nica da classe KledDevUtils.
        ����/// </summary>
        static KledDevUtils instance;

        /// <summary>
        ����/// Cria uma inst�ncia da classe KledDevUtils se nenhuma existir.
        ����/// </summary>
        static void CreateInstance()
        {
            GameObject @object = new("KledDevUtils"); // Cria um GameObject chamado "KledDevUtils".
            @object.AddComponent<KledDevUtils>(); // Adiciona o componente KledDevUtils ao GameObject.
            instance = @object.GetComponent<KledDevUtils>(); // Obt�m a refer�ncia ao componente KledDevUtils.
        }

        /// <summary>
        ����/// Obt�m a inst�ncia da classe KledDevUtils.
        ����/// </summary>
        ����/// <returns>A inst�ncia da classe KledDevUtils.</returns>
        static KledDevUtils GetInstance()
        {
            if (instance == null)
            {
                CreateInstance(); // Cria a inst�ncia se ainda n�o existir.
            }
            return instance;
        }

        /// <summary>
        ����/// Faz uma pausa de x segundos e depois executa a a��o playAction.
        ����/// </summary>
        ����/// <param name="xSeconds">O n�mero de segundos para esperar.</param>
        ����/// <param name="playAction">A a��o a ser executada ap�s a espera.</param>
        public static void WaitXSecondsForPlayAction(float xSeconds, Action playAction)
        {
            if (xSeconds == 0) return;
            GetInstance().StartCoroutine(WaitXSecondsForPLayActionCoroutine(xSeconds, playAction)); // Inicia a coroutine que espera e executa a a��o.
        }

        /// <summary>
        ����/// Remove acentos de uma string.
        ����/// </summary>
        ����/// <param name="text">A string com acentos.</param>
        ����/// <returns>A string sem acentos.</returns>
        public static string RemoveAccents(string text)
        {
            string decomposed = text.Normalize(NormalizationForm.FormD); // Normaliza a string para remover acentos.
            Regex regex = new Regex(@"\p{M}"); // Cria uma express�o regular para identificar caracteres de marca��o de acento.
            string noAccents = regex.Replace(decomposed, string.Empty); // Substitui os caracteres de marca��o de acento por string vazia.
            return noAccents;
        }

        /// <summary>
        ����/// Destroi todos os filhos de um Transform.
        ����/// </summary>
        ����/// <param name="content">O Transform que cont�m os filhos a serem destru�dos.</param>
        public static void ClearChilds(Transform content)
        {
            for (int i = 0; i < content.childCount; i++)
            {
                Destroy(content.GetChild(i).gameObject); // Destroi cada GameObject filho do Transform.
            }
        }

        /// <summary>
        ����/// Preenche um Transform com GameObjects instanciados a partir de um prefab.
        ����/// </summary>
        ����/// <param name="objIDList">Uma lista de IDs de objetos.</param>
        ����/// <param name="content">O Transform que receber� os GameObjects instanciados.</param>
        ����/// <param name="viewObject">O prefab a ser usado para instanciar os GameObjects.</param>
        public static void FillContent(List<int> objIDList, Transform content, GameObject viewObject)
        {

            for (int i = 0; i < objIDList.Count; i++)
            {
                GameObject view = Instantiate(viewObject, content); // Instancia o prefab no Transform.
                view.GetComponent<View>().ID = objIDList[i]; // Define a propriedade ID do componente View com o ID da lista.
            }
        }

        static IEnumerator WaitXSecondsForPLayActionCoroutine(float xSeconds, Action playAction)
        {
            yield return new WaitForSeconds(xSeconds); // Aguarda o n�mero de segundos especificado.
            playAction.Invoke(); // Executa a a��o playAction.
        }
    }
}