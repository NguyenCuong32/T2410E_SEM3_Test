from pymongo import MongoClient

# 1️⃣ KẾT NỐI MONGODB
client = MongoClient("mongodb://localhost:27017/")
db = client["eShop"]
collection = db["OrderCollection"]

print("✅ 1. Kết nối thành công đến MongoDB!")

# 2️⃣ THÊM NHIỀU ĐƠN HÀNG
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
            {"product_id": "vay", "product_name": "dam cong so", "size": "M", "price": 15, "quantity": 1},
            {"product_id": "somi", "product_name": "ao so mi", "size": "L", "price": 11, "quantity": 3}
        ],
        "total_amount": 48,
        "delivery_address": "HCM"
    }
]

collection.insert_many(orders)
print("✅ 2. Đã thêm dữ liệu mẫu vào OrderCollection")

# 3️⃣ CẬP NHẬT ĐỊA CHỈ GIAO HÀNG THEO orderid
collection.update_one({"orderid": 1}, {"$set": {"delivery_address": "Hai Phong"}})
print("✅ 3. Đã cập nhật địa chỉ giao hàng cho orderid = 1")

# 4️⃣ XOÁ MỘT ĐƠN HÀNG
collection.delete_one({"orderid": 2})
print("✅ 4. Đã xoá đơn hàng có orderid = 2")

# 5️⃣ ĐỌC TOÀN BỘ ĐƠN HÀNG
print("\n=== 5. DANH SÁCH ĐƠN HÀNG TRONG OrderCollection ===")
for order in collection.find():
    print(f"\nOrder ID: {order['orderid']} | Delivery: {order['delivery_address']}")
    print("No | Product name | Price | Quantity | Total")
    print("----------------------------------------------")
    total_amount = 0
    for i, p in enumerate(order["products"], start=1):
        total = p["price"] * p["quantity"]
        total_amount += total
        print(f"{i:<2} | {p['product_name']:<12} | {p['price']:<5} | {p['quantity']:<8} | {total}")
    print(f"=> Total amount: {total_amount}")

# 6️⃣ TÍNH TỔNG total_amount CỦA TẤT CẢ ORDER
pipeline = [{"$group": {"_id": None, "sum_amount": {"$sum": "$total_amount"}}}]
result = list(collection.aggregate(pipeline))
if result:
    print(f"\n💰 6. Tổng total_amount của tất cả đơn hàng: {result[0]['sum_amount']}")

# 7️⃣ ĐẾM product_id = 'somi'
count_somi = 0
for order in collection.find():
    for p in order["products"]:
        if p["product_id"] == "somi":
            count_somi += 1

print(f"📦 7. Tổng số sản phẩm có product_id = 'somi': {count_somi}")

print("\n🎯 Hoàn thành toàn bộ yêu cầu bài thi Large Data Management – SET01!")
