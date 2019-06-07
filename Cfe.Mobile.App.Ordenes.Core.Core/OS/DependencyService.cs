using System;
using System.Collections.Generic;
using System.Text;

namespace Cfe.Mobile.App.Ordenes.Core.Core.OS {
    public class DependencyService {
        static Dictionary<Type, Type> dependencies;
        static Dictionary<Type, Type> Dependencies { get { return dependencies ?? (dependencies = new Dictionary<Type, Type>()); } }
        static Dictionary<Type, object> instances;
        static Dictionary<Type, object> Instances { get { return instances ?? (instances = new Dictionary<Type, object>()); } }
        /// <summary>
        /// Registra la clase que se instanciará como dependencia a la interfase
        /// </summary>
        /// <typeparam name="C">Clase</typeparam>
        /// <typeparam name="I">Interfase que implementa</typeparam>
        public static void Register<C, I>() where C : I, new() {
            if (!Dependencies.ContainsKey(typeof(I)))
                Dependencies.Add(typeof(I), typeof(C));
            else
                Dependencies[typeof(I)] = typeof(C);
        }
        /// <summary>
        /// Registra la instancia de un objeto que implementa la interfase
        /// </summary>
        /// <typeparam name="I">Interfase que debe implementar</typeparam>
        /// <param name="instance">Objeto instanciado que implementa la interfase</param>
        public static void Register<I>(I instance) {
            Instances.Add(typeof(I), instance);
        }
        /// <summary>
        /// Obtiene una instancia de la dependencia a la interfase
        /// </summary>
        /// <typeparam name="I">Interfase</typeparam>
        /// <returns></returns>
        public static I Get<I>() {
            if (!Instances.ContainsKey(typeof(I)))
                Instances.Add(typeof(I), (I)Activator.CreateInstance(Dependencies[typeof(I)]));
            return (I)Instances[typeof(I)];
        }
    }
}
