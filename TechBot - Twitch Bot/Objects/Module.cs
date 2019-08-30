namespace TechBot.Objects
{
    public class Module
    {
        ///<summary>
        ///Get Module name
        ///</summary>
        public string Name { get; private set; }

        ///<summary>
        ///Get Module creator
        ///</summary>
        public string Creator { get; private set; }

        ///<summary>
        ///Constructor
        ///</summary>
        public Module(string ModuleName, string Owner)
        {
            Name = ModuleName;
            Creator = Owner;
        }
    }

    /*
    // Collection of Module objects. This class
    // implements IEnumerable so that it can be used
    // with ForEach syntax.
    public class Modules : IEnumerable
    {
        private Module[] _modules;
        public Modules(Module[] mArray)
        {
            _modules = new Module[mArray.Length];

            for (int i = 0; i < mArray.Length; i++)
            {
                _modules[i] = mArray[i];
            }
        }

        // Implementation for the GetEnumerator method.
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        public ModuleEnum GetEnumerator()
        {
            return new ModuleEnum(_modules);
        }
    }

    // When you implement IEnumerable, you must also implement IEnumerator.
    public class ModuleEnum : IEnumerator
    {
        public Module[] _modules;

        // Enumerators are positioned before the first element
        // until the first MoveNext() call.
        int position = -1;

        public ModuleEnum(Module[] list)
        {
            _modules = list;
        }

        public bool MoveNext()
        {
            position++;
            return (position < _modules.Length);
        }

        public void Reset()
        {
            position = -1;
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public Module Current
        {
            get
            {
                try
                {
                    return _modules[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }
    */
}