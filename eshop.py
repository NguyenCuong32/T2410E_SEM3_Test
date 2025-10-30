from flask import Flask, request, jsonify
from mongo_service import OrderService

app = Flask(__name__)
order_service = OrderService()

@app.route('/api/orders/insert', methods=['POST'])
def insert_orders_endpoint():
    return jsonify(order_service.insert_sample_orders())

@app.route('/api/orders/<int:order_id>/address', methods=['PUT'])
def update_address_endpoint(order_id):
    data = request.get_json()
    new_address = data.get('delivery_address')
    if not new_address:
        return jsonify({"status": "error", "message": "Missing 'delivery_address' field in JSON body"}), 400
    return jsonify(order_service.update_delivery_address(order_id, new_address))

@app.route('/api/orders/<int:order_id>', methods=['DELETE'])
def delete_order_endpoint(order_id):
    return jsonify(order_service.delete_order(order_id))

@app.route('/api/products', methods=['GET'])
def get_products_endpoint():
    return jsonify(order_service.get_all_products_details())

@app.route('/api/orders/total-amount', methods=['GET'])
def get_total_amount_endpoint():
    return jsonify(order_service.calculate_total_amount())

@app.route('/api/products/somi/count', methods=['GET'])
def get_somi_count_endpoint():
    return jsonify(order_service.count_somi_products())

if __name__ == '__main__':
    app.run(debug=True, port=5000)