from pymongo import MongoClient
from prettytable import PrettyTable

client = MongoClient("mongodb://localhost:27017/")
db = client["eShop"]
collection = db["OrderCollection"]

collection.delete_many({})

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
            {"product_id": "jean", "product_name": "quan jean", "size": "L", "price": 20, "quantity": 1}
        ],
        "total_amount": 20,
        "delivery_address": "Danang"
    }
]

collection.insert_many(orders)
print("Inserted orders successfully!")

order_id_to_update = 1
new_address = "Ho Chi Minh City"
collection.update_one({"orderid": order_id_to_update}, {"$set": {"delivery_address": new_address}})
print(f"Updated order {order_id_to_update} to address {new_address}")

order_id_to_delete = 2
collection.delete_one({"orderid": order_id_to_delete})
print(f"Deleted order {order_id_to_delete}")

print("\nAll Orders:")
for order in collection.find():
    print(order)

print("All Products in Orders:")
table = PrettyTable(["No", "Product name", "Price", "Quantity", "Total"])

no = 1
for order in collection.find():
    for p in order["products"]:
        total = p["price"] * p["quantity"]
        table.add_row([no, p["product_name"], p["price"], p["quantity"], total])
        no += 1

print(table)
total_amount = 0
for order in collection.find():
    total_amount += order["total_amount"]

print(f"Total Amount of all orders: {total_amount}")
count_somi = 0
for order in collection.find():
    for p in order["products"]:
        if p["product_id"] == "somi":
            count_somi += p["quantity"]
print(f"👕 Total quantity of product_id = 'somi': {count_somi}")