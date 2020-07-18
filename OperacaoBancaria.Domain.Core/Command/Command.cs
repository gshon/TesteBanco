using MediatR;
using OperacaoBancaria.Domain.Core.Events;
using System;

namespace OperacaoBancaria.Domain.Core.Command
{
    public class Command : Message, IRequest
    {
        public DateTime Timestamp { get; private set; }

        public Command()
        {
            Timestamp = DateTime.Now;
        }
    }
}