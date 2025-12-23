import mysql.connector


def connect_server():
    return mysql.connector.connect(
        host="localhost",
        user="root",
        password="",
        port=3306
    )


def connect_db():
    return mysql.connector.connect(
        host="localhost",
        user="root",
        password="",
        database="medical_service",
        port=3306
    )
