using Noobot.Core.MessagingPipeline.Middleware.ValidHandles;
using Noobot.Core.MessagingPipeline.Request;
using Noobot.Core.MessagingPipeline.Response;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Noobot.Core.MessagingPipeline.Middleware.StandardMiddleware
{
    internal class MultipleCommandsMiddleware : MiddlewareBase
    {
        private readonly INoobotCore _noobotCore;

        public MultipleCommandsMiddleware(IMiddleware next, INoobotCore noobotCore) : base(next)
        {
            _noobotCore = noobotCore;

            HandlerMappings = new[]
            {
                new HandlerMapping
                {
                    ValidHandles = new IValidHandle[]
                    {
                        new StartsWithHandle("test")
                    },
                    Description = "Returns supported commands and descriptions of how to use them",
                    EvaluatorFunc = MultipleCommandsHandler
                }
            };
        }

        private IEnumerable<ResponseMessage> MultipleCommandsHandler(IncomingMessage message, IValidHandle matchedHandle)
        {
            var builder = new StringBuilder();
            builder.Append(">>>");

            if (message.TargetedText == "test grej")
            {
                yield return message.ReplyToChannel("EN TESTGREJ!");
            }
            else if (message.TargetedText == "test annat")
            {
                yield return message.ReplyToChannel("ETT ANNAT TEST!");
            }
            else
            {
                IEnumerable<CommandDescription> supportedCommands = GetSupportedCommands().OrderBy(x => x.Command);

                foreach (CommandDescription commandDescription in supportedCommands)
                {
                    string description = commandDescription.Description.Replace("@{bot}", $"@{_noobotCore.GetBotUserName()}");
                    builder.AppendFormat("{0}\t- {1}\n", commandDescription.Command, description);
                }
            }

            yield return message.ReplyToChannel(builder.ToString());
        }
    }
}