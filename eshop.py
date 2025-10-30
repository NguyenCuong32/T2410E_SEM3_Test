from flask import Flask, request, jsonify
from pymongo import MongoClient


client = MongoClient("mongodb://localhost:27017/")
db = client["eShop"]
collection = db["OrderCollection"]

app = Flask(__name__)


@app.route('/orders', methods=['GET'])
def get_orders():
    orders = []
    for order in collection.find():
        order['_id'] = str(order['_id'])  
        orders.append(order)
    return jsonify(orders), 200


@app.route('/orders', methods=['POST'])
def insert_orders():
    data = request.get_json()
    if isinstance(data, list):
        collection.insert_many(data)
        return jsonify({"message": "Inserted multiple orders"}), 201
    else:
        collection.insert_one(data)
        return jsonify({"message": "Inserted one order"}), 201


@app.route('/orders/<int:orderid>', methods=['PUT'])
def update_delivery(orderid):
    new_address = request.json.get("delivery_address")
    result = collection.update_one({"orderid": orderid}, {"$set": {"delivery_address": new_address}})
    if result.modified_count > 0:
        return jsonify({"message": "Address updated successfully"}), 200
    else:
        return jsonify({"message": "Order not found"}), 404


@app.route('/orders/<int:orderid>', methods=['DELETE'])
def delete_order(orderid):
    result = collection.delete_one({"orderid": orderid})
    if result.deleted_count > 0:
        return jsonify({"message": "Order deleted successfully"}), 200
    else:
        return jsonify({"message": "Order not found"}), 404


@app.route('/orders/table', methods=['GET'])
def show_table():
    data = []
    for order in collection.find():
        for i, p in enumerate(order['products'], start=1):
            data.append({
                "No": i,
                "Product name": p["product_name"],
                "Price": p["price"],
                "Quantity": p["quantity"],
                "Total": p["price"] * p["quantity"]
            })
    return jsonify(data), 200


@app.route('/orders/total', methods=['GET'])
def total_amount():
    total_sum = 0
    for doc in collection.find():
        total_sum += doc.get("total_amount", 0)
    return jsonify({"total_amount": total_sum}), 200


@app.route('/orders/count_somi', methods=['GET'])
def count_somi():
    count = collection.count_documents({"products.product_id": "somi"})
    return jsonify({"count_somi": count}), 200


if __name__ == '__main__':
    app.run(debug=True)
