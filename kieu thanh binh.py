1. Install MongoDB Driver (Python)
pip install pymongo

2. Python Code Example (Large Data Management – MongoDB)

File: eshop_mongo.py

from pymongo import MongoClient

# -----------------------------
# 1. Connect to MongoDB
# -----------------------------
client = MongoClient("mongodb://localhost:27017/")
db = client["eShop"]                    # database name
order_collection = db["OrderCollection"]  # collection name


# -----------------------------
# 2. Insert Many Documents
# -----------------------------
def insert_orders():
    documents = [
        {
            "orderid": 1,
            "products": [
                {"product_id": "quanau", "product_name": "quan au", "size": "XL", "price": 10, "quantity": 1},
                {"product_id": "somi",   "product_name": "ao so mi", "size": "XL", "price": 10.5, "quantity": 2}
            ],
            "total_amount": 31,
            "delivery_address": "Hanoi"
        },
        {
            "orderid": 2,
            "products": [
                {"product_id": "jean", "product_name": "quan jean", "size": "L", "price": 15, "quantity": 1}
            ],
            "total_amount": 15,
            "delivery_address": "HCM"
        }
    ]

    order_collection.insert_many(documents)
    print("Inserted orders successfully!")


# -----------------------------
# 3. Update delivery address by orderid
# -----------------------------
def update_address(orderid, new_address):
    order_collection.update_one(
        {"orderid": orderid},
        {"$set": {"delivery_address": new_address}}
    )
    print(f"Updated address of orderid = {orderid}")


# -----------------------------
# 4. Remove an Order
# -----------------------------
def remove_order(orderid):
    order_collection.delete_one({"orderid": orderid})
    print(f"Deleted order orderid = {orderid}")


# -----------------------------
# 5. Read All Orders (table format)
# -----------------------------
def read_orders():
    cursor = order_collection.find()
    print("\n===== ORDER DETAILS (PRODUCT LIST) =====")
    print(f"{'No':<4} {'Product Name':<15} {'Price':<10} {'Quantity':<10} {'Total':<10}")

    row_num = 1
    for order in cursor:
        for item in order["products"]:
            total = item["price"] * item["quantity"]
            print(f"{row_num:<4} {item['product_name']:<15} {item['price']:<10} {item['quantity']:<10} {total:<10}")
            row_num += 1


# -----------------------------
# Demo execution
# -----------------------------
insert_orders()
update_address(1, "Da Nang")
remove_order(2)
read_orders()

 Output (when reading)
===== ORDER DETAILS (PRODUCT LIST) =====
No   Product Name    Price      Quantity   Total
1    quan au         10          1          10
2    ao so mi        10.5        2          21