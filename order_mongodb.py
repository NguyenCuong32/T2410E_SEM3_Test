from pymongo import MongoClient

# 1️⃣ Kết nối tới MongoDB
client = MongoClient("mongodb://localhost:27017/")
db = client["eShop"]
orders = db["OrderCollection"]

# 2️⃣ Thêm nhiều document
sample_orders = [
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
            {"product_id": "vest", "product_name": "ao vest", "size": "L", "price": 20, "quantity": 1},
            {"product_id": "somi", "product_name": "ao so mi", "size": "M", "price": 10.5, "quantity": 3}
        ],
        "total_amount": 51.5,
        "delivery_address": "Haiphong"
    }
]

orders.insert_many(sample_orders)
print("✅ Inserted sample orders!")

# 3️⃣ Cập nhật địa chỉ giao hàng theo orderid
orders.update_one({"orderid": 1}, {"$set": {"delivery_address": "Danang"}})
print("✅ Updated delivery address for orderid = 1")

# 4️⃣ Xóa một đơn hàng
orders.delete_one({"orderid": 2})
print("✅ Deleted order with orderid = 2")

# 5️⃣ Đọc tất cả đơn hàng và in ra bảng
print("\n📦 All Orders:")
for order in orders.find():
    print(f"\nOrder ID: {order['orderid']}")
    print(f"Delivery Address: {order['delivery_address']}")
    print("No | Product Name | Price | Quantity | Total")
    print("---------------------------------------------")
    for i, p in enumerate(order['products'], start=1):
        total = p['price'] * p['quantity']
        print(f"{i:<3}| {p['product_name']:<13}| {p['price']:<6}| {p['quantity']:<8}| {total}")
    print("---------------------------------------------")
    print(f"Total Amount: {order['total_amount']}")
    print("---------------------------------------------")

# 6️⃣ Tính tổng số tiền của tất cả đơn hàng
pipeline = [{"$group": {"_id": None, "total": {"$sum": "$total_amount"}}}]
result = list(orders.aggregate(pipeline))
if result:
    print(f"\n💰 Total amount of all orders: {result[0]['total']}")

# 7️⃣ Đếm tổng số sản phẩm có product_id = 'somi'
count_somi = orders.aggregate([
    {"$unwind": "$products"},
    {"$match": {"products.product_id": "somi"}},
    {"$group": {"_id": None, "total_somi": {"$sum": "$products.quantity"}}}
])

for doc in count_somi:
    print(f"🧮 Total quantity of 'somi': {doc['total_somi']}")
