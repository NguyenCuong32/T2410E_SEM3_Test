from db.connection import get_database

def insert_orders():
    db = get_database()
    collection = db["OrderCollection"]

    orders = [
        {
            "orderid": 1,
            "products": [
                {"product_id": "quanau", "product_name": "quan au", "size": "XL", "price": 10, "quantity": 1},
                {"product_id": "somi", "product_name": "ao so mi", "size": "XL", "price": 10.5, "quantity": 2}
            ],
            "total_amount": 31,
            "delivery_address": "Hanoi"
        },
        {
            "orderid": 2,
            "products": [
                {"product_id": "giay", "product_name": "giay tay", "size": "42", "price": 20, "quantity": 1},
                {"product_id": "somi", "product_name": "ao so mi", "size": "L", "price": 11, "quantity": 1}
            ],
            "total_amount": 31,
            "delivery_address": "HCM"
        }
    ]

    collection.insert_many(orders)
    print("✅ Inserted sample orders successfully.")
#  câu 2