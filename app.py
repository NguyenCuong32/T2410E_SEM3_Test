from services.insert_orders import insert_orders
from services.edit_address import edit_delivery_address
from services.remove_order import remove_order
from services.read_orders import read_all_orders
from services.calculate_total import calculate_total_amount
from services.count_products import count_product_id

if __name__ == "__main__":
    print("=== ESHOP SYSTEM START ===")

    # Câu 2: Thêm dữ liệu mẫu
    insert_orders()

    # Câu 3: Chỉnh sửa địa chỉ giao hàng
    edit_delivery_address(1, "Ha Noi Capital")

    # Câu 4: Xóa 1 đơn hàng
    remove_order(2)

    # Câu 5: Đọc tất cả đơn hàng
    read_all_orders()

    # Câu 6: Tính tổng amount
    calculate_total_amount()

    # Câu 7: Đếm sản phẩm có product_id = somi
    count_product_id("somi")

    print("=== ESHOP SYSTEM END ===")
