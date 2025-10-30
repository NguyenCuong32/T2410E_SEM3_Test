from db.connection import get_database

def read_all_orders():
    db = get_database()
    collection = db["OrderCollection"]

    print("\n📋 All orders:")
    for order in collection.find():
        print(f"Order ID: {order['orderid']}")
        print("No | Product Name | Price | Quantity | Total")
        total_amount = 0
        for i, p in enumerate(order["products"], start=1):
            total = p["price"] * p["quantity"]
            total_amount += total
            print(f"{i} | {p['product_name']} | {p['price']} | {p['quantity']} | {total}")
        print(f"Total amount: {total_amount}")
        print("-" * 40)
# câu 5