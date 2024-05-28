using Scheduler.Domain.Models;

namespace Scheduler.Api.ViewModels;

public sealed record RoomsViewModel(int Id, string Name, RoomStatus Status);
