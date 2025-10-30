from flask import Flask, jsonify, request
from pymongo import MongoClient

app = Flask(__name__)

client = MongoClient("mongodb://localhost:27017/")
db = client["eShop"]
orders = db["OrderCollection"]

@app.route("/orders", methods=["POST"])
def create_order():
    data = request.get_json()
    if not data:
        return jsonify({"error": "Missing JSON body"}), 400

    orders.insert_one(data)
    return jsonify({"message": "Order added successfully"}), 201

@app.route("/orders", methods=["GET"])
def get_all_orders():
    result = []
    for order in orders.find():
        order["_id"] = str(order["_id"])
        result.append(order)
    return jsonify(result)

@app.route("/orders/<int:orderid>", methods=["PUT"])
def update_order(orderid):
    data = request.get_json()
    new_address = data.get("delivery_address")
    if not new_address:
        return jsonify({"error": "Missing delivery_address"}), 400

    res = orders.update_one({"orderid": orderid}, {"$set": {"delivery_address": new_address}})
    if res.modified_count == 0:
        return jsonify({"message": "Order not found"}), 404
    return jsonify({"message": "Updated successfully"})

@app.route("/orders/<int:orderid>", methods=["DELETE"])
def delete_order(orderid):
    res = orders.delete_one({"orderid": orderid})
    if res.deleted_count == 0:
        return jsonify({"message": "Order not found"}), 404
    return jsonify({"message": f"Deleted order {orderid}"})

@app.route("/orders/total", methods=["GET"])
def get_total_amount():
    total_sum = sum(order["total_amount"] for order in orders.find())
    return jsonify({"total_amount_sum": total_sum})

@app.route("/orders/count/somi", methods=["GET"])
def count_somi():
    count = 0
    for order in orders.find():
        for p in order["products"]:
            if p["product_id"] == "somi":
                count += 1
    return jsonify({"somi_count": count})

if __name__ == "__main__":
    app.run(debug=True, port=5001)
