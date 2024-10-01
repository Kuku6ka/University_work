from bs4 import BeautifulSoup
from selenium import webdriver
from selenium.webdriver.firefox.service import Service
from webdriver_manager.firefox import GeckoDriverManager
from selenium.webdriver.common.by import By
from selenium.webdriver.support import expected_conditions as EC
from selenium.webdriver.firefox.options import Options
from selenium.webdriver.support.ui import WebDriverWait
import re


def extract_product_article(url):
    match = re.search(r'/catalog/(\d+)/', url)
    if match:
        return match.group(1)
    else:
        return None

def is_valid(item):
    try:
        if not all([item['article'], item['name'], item['price'], item['rating'], item['reviews_numb']]):
            return False
        elif not item['price'].isdigit():
            return False
        elif not item['rating'].replace('.', '', 1).isdigit():
            return False
        elif not item["reviews_numb"].isdigit():
            return False

        return True

    except Exception as error:
        print(f"Непредвиденная ошибка: {error}")
        return False


def filter_data(data, min_cost=None, max_cost=None, min_rating=None):
    new_data = []
    for item in data:
        cost = int(item['price'])
        rating = float(item['rating'])
        if min_cost and cost < min_cost:
            continue
        if max_cost and cost > max_cost:
            continue
        if min_rating and rating < min_rating:
            continue
        new_data.append(item)
    return new_data


def main():
    #настройки selenium
    firefox_options = Options()
    firefox_options.add_argument("--headless")

    driver = webdriver.Firefox(service=Service(GeckoDriverManager().install()), options=firefox_options)

    page = 1
    data = []

    while True:
        driver.get(f"https://www.wildberries.ru/catalog/mebel/divany-i-kresla/divany?sort=popular&page={page}")
        try:
            WebDriverWait(driver, 20).until(EC.presence_of_element_located((By.CSS_SELECTOR, ".product-card")))
        except:
            break

        html_content = driver.page_source

        soup = BeautifulSoup(html_content, 'html.parser')
        div_class = 'product-card__wrapper'
        items = soup.find_all('div', class_=div_class)

        for item in items:
            article = extract_product_article(item.find('a', class_="product-card__link").get("href"))

            name = item.find('a', class_="product-card__link").get("aria-label")

            price_text = item.find('span', class_='price__wrap').find('ins', class_='price__lower-price').text.strip()
            price_text = price_text.replace('\xa0', ' ')
            price_number = re.sub(r'\D', '', price_text)

            rating = item.find('span', class_='address-rate-mini').text.strip()

            reviews_numb = item.find('span', class_='product-card__count').text.strip()
            reviews_numb = re.sub(r'\D', '', reviews_numb)

            product_data = {
                "article": article,
                "name": name,
                "price": price_number,
                "rating": rating,
                "reviews_numb": reviews_numb
            }

            if is_valid(product_data):
                data.append(product_data)

        page += 1

    filtered = filter_data(data, 5000, 15000, 4)

    with open("list.txt", "w+") as file:
        for item in filtered:
            file.write(f"ID: {item['article']}, Name: {item['name']}, Price: {item['price']}, Rating: {item['rating']}, Reviews: {item['reviews_numb']}\n")

    driver.quit()


main()