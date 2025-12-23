import mysql.connector
from mysql.connector import Error
from dotenv import load_dotenv
import os
load_dotenv()
def create_connection():
    host = os.getenv('DB_HOST')
    database = os.getenv('DB_NAME')
    user = os.getenv('DB_USER')
    password = os.getenv('DB_PASSWORD')
    if not all([host, database, user]): 
        print("Error: .env file is not fully configured")
        return None
    db_config = {
        'host': host,
        'database': database,
        'user': user,
        'password': password
    }
    conn = None
    try:
        conn = mysql.connector.connect(**db_config)
    except Error as e:
        print(f"Error occured: {e}")
    return conn