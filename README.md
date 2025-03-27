# HW Task - Order aggregator 

- Publishes RESP API endpoint accepting one or more ordrs in format:
  ```json
  [
    {
      "productId": "456",
      "quantity": 5
    },
    {
      "productId": "789",
      "quantity": 42
    }
  ]
  ```
- Order are aggregated - sum of quntity for specific productId.
- Aggregated orders are send to external system - not more often than 20 seconds - for this purposes they are only written into console.
