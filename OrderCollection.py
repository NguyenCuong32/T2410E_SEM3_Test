from pymongo import MongoClient

client = MongoClient("mongodb://localhost:27017")
db = client["eShop"]
collection = db["OrderCollection"]

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
            },
            {
                "product_id": "somi",
                "product_name": "so mi xin",
                "size": "XXL",
                "price": 15,
                "quantity": 3
            }
        ],
        "total_amount": 46,
        "delivery_address": "Hanoi"
    }
]

collection.insert_many(orders)


collection.update_one(
    {"orderid": 1},
    {"$set": {"delivery_address": "Ho Chi Minh"}}
)

collection.delete_one({"orderid": 1})

orders = collection.find()
no = 1
for order in orders:
    for product in order["products"]:
        total = product["price"] * product["quantity"]
        print(f"{no} {product['product_name']} {product['price']} {product['quantity']} {total}")
        no += 1

total_all = sum(order["total_amount"] for order in collection.find())
print(f"Tổng tiền tất cả đơn hàng: {total_all}")

count = 0
for order in collection.find():
    for product in order["products"]:
        if product["product_id"] == "somi":
            count += product["quantity"]
print(f"Tổng số sản phẩm 'somi': {count}")


def insert_order(order):
    collection.insert_one(order)
    print("Đã thêm đơn hàng.")


#them
new_order = {
    "orderid": 2,
    "products": [
        {
            "product_id": "jeans",
            "product_name": "quan jeans",
            "size": "L",
            "price": 15,
            "quantity": 1
        }
    ],
    "total_amount": 15,
    "delivery_address": "Da Nang"
}

insert_order(new_order)




#xoa
def delete_order(orderid):
    result = collection.delete_one({"orderid": 2})
    if result.deleted_count:
        print("Đã xóa đơn hàng.")
    else:
        print("Không tìm thấy đơn hàng.")

delete_order(2)
