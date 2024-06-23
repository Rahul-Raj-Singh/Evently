using Evently.Common.Application.Caching;

namespace Evently.Modules.Ticketing.Application.Carts;

public class CartService(ICacheService cacheService)
{
    private readonly TimeSpan ExpirationTime = TimeSpan.FromMinutes(20);
    public async Task<Cart> GetAsync(Guid customerId, CancellationToken cancellationToken = default)
    {
        var key = GetCacheKey(customerId);

        return await cacheService.GetAsync<Cart>(key, cancellationToken) ?? Cart.CreateDefault(customerId);
    }
    public async Task ClearAsync(Guid customerId, CancellationToken cancellationToken = default)
    {
        var key = GetCacheKey(customerId);

        await cacheService.SetAsync(key, Cart.CreateDefault(customerId), ExpirationTime, cancellationToken);
    }
    public async Task AddItemAsync(Guid customerId, CartItem cartItem, CancellationToken cancellationToken = default)
    {
        var key = GetCacheKey(customerId);

        var cart = await GetAsync(customerId, cancellationToken);

        var existingItem = cart.CartItems.Find(x => x.TicketTypeId == cartItem.TicketTypeId);
        
        if (existingItem is null) 
            cart.CartItems.Add(cartItem);
        else 
            existingItem.Quantity += cartItem.Quantity;
        
        await cacheService.SetAsync(key, cart, ExpirationTime, cancellationToken);
    }
    public async Task RemoveItemAsync(Guid customerId, Guid ticketTypeId, CancellationToken cancellationToken = default)
    {
        var key = GetCacheKey(customerId);

        var cart = await GetAsync(customerId, cancellationToken);

        var existingItem = cart.CartItems.Find(x => x.TicketTypeId == ticketTypeId);

        if (existingItem is null) return;
        
        cart.CartItems.Remove(existingItem);

        await cacheService.SetAsync(key, cart, ExpirationTime, cancellationToken);
    }

    private static string GetCacheKey(Guid customerId) => $"carts:{customerId}";
}
