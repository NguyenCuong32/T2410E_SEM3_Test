from pymongo import MongoClient
from prettytable import PrettyTable

client = MongoClient("mongodb://localhost:27017")
db = client["eShop"]
orders = db["OrderCollection"]


orders.create_index("orderid", unique=True)

def insert_orders():
    docs = [
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
                {"product_id": "giay", "product_name": "giay the thao", "size": "42", "price": 13, "quantity": 2}
            ],
            "total_amount": 26,
            "delivery_address": "Ho Chi Minh"
        }
    ]

    for doc in docs:
        
        orders.replace_one({"orderid": doc["orderid"]}, doc, upsert=True)

    print("Đã thêm hoặc cập nhật các đơn hàng mẫu vào database.")


def update_address(orderid, new_address):
    order = orders.find_one({"orderid": orderid})
    if not order:
        print("Không tìm thấy orderid.")
        return

    order["delivery_address"] = new_address
    
    orders.replace_one({"orderid": orderid}, order)
    print(f"Đã cập nhật địa chỉ cho orderid {orderid}.")


insert_orders()

while True:
    print("\nMENU")
    print("1. Cập nhật địa chỉ giao hàng")
    print("2. Xóa đơn hàng")
    print("3. Xem tất cả đơn hàng")
    print("4. Tính tổng total_amount")
    print("5. Đếm số sản phẩm 'somi'")
    print("0. Thoát")
    choice = input("Chọn: ")

    if choice == "1":
        orderid = int(input("Nhập orderid: "))
        address = input("Nhập địa chỉ mới: ")
        update_address(orderid, address)
    elif choice == "2":
        orderid = int(input("Nhập orderid để xóa: "))
        result = orders.delete_one({"orderid": orderid})
        print("Đã xóa." if result.deleted_count else "Không tìm thấy orderid.")
    elif choice == "3":
        cursor = orders.find({})
        table = PrettyTable(["No", "Order ID", "Product name", "Price", "Quantity", "Total", "Address"])
        no = 1
        for order in cursor:
            for p in order["products"]:
                total = p["price"] * p["quantity"]
                table.add_row([no, order["orderid"], p["product_name"], p["price"], p["quantity"], total, order["delivery_address"]])
                no += 1
        print(table)
    elif choice == "4":
        total_sum = sum(o.get("total_amount", 0) for o in orders.find({}))
        print(f"Tổng toàn bộ đơn hàng: {total_sum}")
    elif choice == "5":
        count = sum(p["quantity"] for o in orders.find({}) for p in o["products"] if p["product_id"] == "somi")
        print(f"Tổng số sản phẩm 'somi': {count}")
    elif choice == "0":
        break
    else:
        print("Lựa chọn không hợp lệ, thử lại.")
