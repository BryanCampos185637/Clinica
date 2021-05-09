using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace UserInterface.Controllers
{
    public class MiControladorBaseController : Controller
    {
        private IMediator mediator;
        protected IMediator _mediator => mediator ?? (mediator = HttpContext.RequestServices.GetService<IMediator>());
    }
}
