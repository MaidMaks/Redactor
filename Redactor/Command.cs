using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redactor
{
    abstract class Command //Создание фигуры и изменение. Список фигур для создания и удаления 
    {
        public abstract void Redo();

        public abstract void Undo();
    }

    class ConcreteCommand : Command
    {
        Receiver receiver;
        public ConcreteCommand(Receiver r)
        {
            receiver = r;
        }

        public override void Redo()
        {
            receiver.Operation();
        }

        public override void Undo()
        {

        }

    }

    class Receiver
    {
        public void Operation()
        {
        
        }
    }

    class Invoker
    {
        Command command;
        public void SetCommand(Command c)
        {
            command = c;
        }
        public void Run()
        {
            command.Redo();
        }
        public void Cancel()
        {
            command.Undo();
        }
    }
}
