
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WORDLE
{
    public class WordleManager : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private ObservableCollection<string> _historico;
        private string[] _secuencia = new string [5];


        public WordleManager()
           
        {
            string [] secuencia = { "b", "e", "s", "a", "r" };
            for (int i = 0; i < 5; i++)
            {
                _secuencia[i] = secuencia[i];
            }

        }
        public ObservableCollection<string> Historico
        {
            get
            { return _historico; }
            set
            { _historico = value; }
        }

        public string Evaluar(int[] intento) {

            string info = "";
            int coincidencias_exactas = 0;
            int coincidencias_parciales = 0;
            bool[] confirmados = new bool[5];

            //Comprobar coincidencias exactas
            for (int i = 0; i < 5; i++)
            {
                if (intento[i] == _secuencia[i])
                {
                    coincidencias_exactas++;
                    confirmados[i] = true;
                }
            }
            //Si el intento == secuencia_objetivo, entonces OK, juego terminado
            if (coincidencias_exactas == 5)
            {
                info = "ENHORABUENA. La secuencia es correcta";
            }
            else
            {
                for (int  i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (confirmados[i] == false &&
                           _secuencia[i] == intento[j] &&
                            i != j)
                        {
                            coincidencias_parciales++;
                        }
                    }
                }
                info = "Has encontrado " + coincidencias_exactas + " coincidencias exactas" +
                        " y " + coincidencias_parciales + " coincidencias parciales.";
            }
            return info;
        }

        public string IntroducirIntento(int[] intento)
        {
            //añadir a la coleccion obervable de historico ese valor intentado
            Historico.Add(intento[0].ToString() + " " +
                          intento[1].ToString() + " " +
                          intento[2].ToString() + " " +
                          intento[3].ToString() + " " +
                          intento[4].ToString());

            //EVALUAR ACIERTO
            return Evaluar(intento);
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }


}
