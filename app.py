from pymongo import MongoClient


client = MongoClient("mongodb://localhost:27017/")
db = client["eShop"]
order_collection = db["OrderCollection"]


order_collection.delete_many({})


orders = [
    {
        "orderid": 1,
        "products": [
            {
                "product_id": "quanau",
                "product_name": "quan au",
                "size": "XL",
                "price": 10,
                "quantity": 1
            },
            {
                "product_id": "somi",
                "product_name": "ao so mi",
                "size": "XL",
                "price": 10.5,
                "quantity": 2
            }
        ],
        "total_amount": 31,
        "delivery_address": "Hanoi"
    },
    {
        "orderid": 2,
        "products": [
            {
                "product_id": "somi",
                "product_name": "ao so mi",
                "size": "L",
                "price": 11,
                "quantity": 1
            }
        ],
        "total_amount": 11,
        "delivery_address": "HCM"
    }
]
order_collection.insert_many(orders)
print(" Inserted sample orders!")


order_collection.update_one(
    {"orderid": 1},
    {"$set": {"delivery_address": "Ha Noi Capital"}}
)
print(" Updated delivery address for orderid=1")


order_collection.delete_one({"orderid": 2})
print(" Deleted orderid=2")


print("\n All Orders:")
cursor = order_collection.find()
for order in cursor:
    print(f"Order ID: {order['orderid']}, Address: {order['delivery_address']}")
    print(f"{'No':<3}{'Product name':<15}{'Price':<10}{'Quantity':<10}{'Total':<10}")
    total_amount = 0
    for i, p in enumerate(order['products'], 1):
        total = p['price'] * p['quantity']
        total_amount += total
        print(f"{i:<3}{p['product_name']:<15}{p['price']:<10}{p['quantity']:<10}{total:<10}")
    print(f" Total amount: {total_amount}\n")


pipeline = [
    {"$group": {"_id": None, "total_sum": {"$sum": "$total_amount"}}}
]
result = list(order_collection.aggregate(pipeline))
print(" Tổng total_amount:", result[0]['total_sum'])


pipeline = [
    {"$unwind": "$products"},
    {"$match": {"products.product_id": "somi"}},
    {"$group": {"_id": None, "total_quantity": {"$sum": "$products.quantity"}}}
]
result = list(order_collection.aggregate(pipeline))
if result:
    print(" Tổng số lượng sản phẩm 'somi':", result[0]['total_quantity'])
else:
    print(" Không tìm thấy sản phẩm 'somi'")
