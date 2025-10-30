from flask import Flask, jsonify, request
from pymongo import MongoClient

app = Flask(__name__)

# connection
client = MongoClient("mongodb://localhost:27017/")
db = client["eShop"]
OrderCollection = db["OrderCollection"]

# samples
SAMPLE_ORDERS = [
    {
        "orderid": 1,
        "products": [
            {"product_id": "quanau", "product_name": "quan au", "size": "XL", "price": 10.0, "quantity": 1},
            {"product_id": "somi", "product_name": "ao so mi", "size": "XL", "price": 10.5, "quantity": 2},
        ],
        "total_amount": 31.0,
        "delivery_address": "Hanoi",
    }
]

#post
@app.route("/seed", methods=["POST"])
def seed_data():
    OrderCollection.delete_many({})
    OrderCollection.insert_many(SAMPLE_ORDERS)
    return jsonify({"message": "Seeded sample orders"}), 201

#get
@app.route("/orders", methods=["GET"])
def get_orders():
    orders = list(OrderCollection.find({}, {"_id": 0}))
    return jsonify(orders)


#put
@app.route("/orders/<int:orderid>/address", methods=["PUT"])
def update_address(orderid):
    new_addr = request.json.get("delivery_address")
    OrderCollection.update_one({"orderid": orderid}, {"$set": {"delivery_address": new_addr}})
    return jsonify({"message": "Updated", "orderid": orderid, "new_address": new_addr})


#delete
@app.route("/orders/<int:orderid>", methods=["DELETE"])
def delete_order(orderid):
    OrderCollection.delete_one({"orderid": orderid})
    return jsonify({"message": "Deleted", "orderid": orderid})


#get
@app.route("/orders/table", methods=["GET"])
def orders_table():
    orders = list(OrderCollection.find({}))
    text = ""
    for o in orders:
        text += f"Order ID: {o['orderid']} | Address: {o['delivery_address']}\n"
        text += "No  Product name      Price   Quantity   Total\n"
        total = 0
        for i, p in enumerate(o['products'], start=1):
            t = p['price'] * p['quantity']
            total += t
            text += f"{i:<3} {p['product_name']:<15} {p['price']:<7} {p['quantity']:<9} {t}\n"
        text += f"Total Amount: {total}\n\n"
    return text, 200, {"Content-Type": "text/plain; charset=utf-8"}

@app.route("/orders/total_amount", methods=["GET"])
def total_amount():
    total = sum(o["total_amount"] for o in OrderCollection.find({}))
    return jsonify({"total_amount_all_orders": total})

@app.route("/orders/count_somi", methods=["GET"])
def count_somi():
    count = OrderCollection.count_documents({"products.product_id": "somi"})
    return jsonify({"orders_containing_somi": count})


if __name__ == "__main__":
    app.run(debug=True)
