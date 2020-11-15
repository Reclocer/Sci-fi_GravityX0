using System.Collections.Generic;

namespace Corebin.Core.LinkedLists
{
    public sealed class DoublyLinkedStacks<T>
    {
        public T SelectedObj => _selectedObj;
        private T _selectedObj;

        private Stack<T> _stack1;
        private Stack<T> _stack2;

        public DoublyLinkedStacks(List<T> list1, List<T> list2)
        {
            _stack1 = new Stack<T>();
            _stack2 = new Stack<T>();

            ToCollectStack(list1, list2);
        }        
        
        /// <summary>
        /// Select next object
        /// </summary>
        /// <returns>Return next or selected, if next == null</returns>
        public T Next()
        {    
            try 
            {
                T obj = _stack2.Pop();

                if (_selectedObj != null)
                {
                    _stack1.Push(_selectedObj);
                }

                _selectedObj = obj;
                return obj;
            }
            catch
            {
                return _selectedObj;
            }
        }

        /// <summary>
        /// Select previous object
        /// </summary>
        /// <returns>Return previous or selected, if previous == null</returns>
        public T Previous()
        {
            try
            {
                T obj = _stack1.Pop();

                if (_selectedObj != null)
                {
                    _stack2.Push(_selectedObj);
                }

                _selectedObj = obj;
                return obj;
            }
            catch
            {
                return _selectedObj;
            }            
        }

        /// <summary>
        /// Replacement for new linked stacks
        /// </summary>
        /// <param name="list1"></param>
        /// <param name="list2"></param>
        public void ToLinkedList(List<T> list1, List<T> list2)
        {
            _stack1.Clear();
            _stack2.Clear();

            ToCollectStack(list1, list2);
        } 
        
        private void ToCollectStack(List<T> list1, List<T> list2)
        {
            list1.ForEach(obj => _stack1.Push(obj));
            list2.ForEach(obj => _stack1.Push(obj));
               
            for(int i= 0; i <= _stack1.Count; i++)
            {
                T obj = _stack1.Pop();
                _stack2.Push(obj);
            }

            _selectedObj = list1[0];            
        }
    }
}
