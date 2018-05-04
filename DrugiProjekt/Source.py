#!/usr/bin/env python

import matplotlib.pyplot as plt
from skimage import data, io
import numpy
import cv2
from scipy import ndimage
from PIL import Image


def generate_horizontal_line(image, line_size_x):
    return generate_line(image, 0, image.height / 2 - line_size_x / 2, image.width, image.height / 2 + line_size_x / 2)


def generate_vertical_line(image, line_size_y):
    return generate_line(image, image.width / 2 - line_size_y / 2, 0, image.width / 2 + line_size_y / 2, image.height)


def generate_line(image, start_x, start_y, end_x, end_y):
    crop_mask = (start_x, start_y, end_x, end_y)
    crop_img = image.crop(crop_mask)
    return crop_img


def generate_fragment(image, x, y, k):
    return generate_line(image, x, y, x + k, y + k)


def convert_image_to_array(img):
    return numpy.asarray(img.convert("L"))


def calculate_hu_moments(tab):
    return cv2.HuMoments(cv2.moments(tab)).flatten()


def get_middle_pixel_color(imge, size):
    return imge.load()[size / 2, size / 2]


def get_variation_color(img):
    return numpy.var(convert_image_to_array(img))


def generate_parameters_for_knn(fragment, size):
    image_array = convert_image_to_array(fragment)
    moments = calculate_hu_moments(image_array)
    middle_pixel_color = get_middle_pixel_color(fragment, size)
    variation = get_variation_color(fragment)
    brightness = 0  # TODO wyliczenie jasnosci obrazu
    edges = 0  # TODO wykrycie krawedzi przy uzyciu canny z cv
    variation_of_vertical_line = get_variation_color(generate_vertical_line(fragment, 4))
    variation_of_horizontal_line = get_variation_color(generate_horizontal_line(fragment, 4))

    params = [
        {"id": 1, "name": "moments", "value": moments},
        {"id": 2, "name": "middle_pixel_color", "value": middle_pixel_color},
        {"id": 3, "name": "variation", "value": variation},
        {"id": 4, "name": "brightness", "value": brightness},
        {"id": 5, "name": "edges", "value": edges},
        {"id": 6, "name": "variation_of_vertical_line", "value": variation_of_vertical_line},
        {"id": 7, "name": "variation_of_horizontal_line", "value": variation_of_horizontal_line}
    ]

    generate_attributes_vector(params, 7)


def add_value_to_array(array, value):
    return array + [value]


def generate_attributes_vector(params, params_amount):
    array = []
    for i in range(params_amount):
        if hasattr(params[i]["value"], "__len__"):
            for value in params[i]["value"]:
                array = add_value_to_array(array, value)
        else:
            array = add_value_to_array(array, params[i]["value"])

    print(array)


def generate():
    im = Image.open('healthy/01_h.jpg')
    pix = im.load()
    h, w = im.size

    for y in range(1, h - 1):
        for x in range(1, w - 1):
            value_max = max(pix[y - 1, x - 1][0], pix[y - 1, x][0], pix[y - 1, x + 1][0], pix[y, x - 1][0],
                            pix[y, x][0], pix[y, x + 1][0], pix[y + 1, x - 1][0], pix[y + 1, x][0],
                            pix[y + 1, x + 1][0])
            if value_max == pix[y, x][0]:
                pix[y, x] = (255, 255, 255)
    im.save("01_h.png")


def fun():
    plt.figure(figsize=(16, 16))
    image = ['healthy/01_h.jpg']
    for i in image:
        plt.subplot(3, 1, 1)
        plt.axis('off')
        io.imshow(data.imread(i, as_grey=False))

        plt.subplot(3, 1, 2)
        plt.axis('off')
        grey = data.imread(i, as_grey=True)
        dx = ndimage.sobel(grey, 1)
        dy = ndimage.sobel(grey, 0)
        mag = numpy.hypot(dx, dy)
        mag *= 255.0 / numpy.max(mag)
        kernel = numpy.ones((5, 5), numpy.uint8)
        erosion = cv2.erode(mag, kernel, iterations=1)
        dilation = cv2.dilate(erosion, kernel, iterations=1)
        io.imshow(dilation)

        plt.subplot(3, 1, 3)
        plt.axis('off')
        io.imshow(data.imread(i, as_grey=True))

        plt.show()


def create_path_for_image(number):
    return 'healthy/0' + str(number) + '_h.jpg' if number < 10 else 'healthy/' + str(number) + '_h.jpg'


def generate_images_paths(amount):
    images_paths = []
    for i in range(1,16):
        images_paths = images_paths + [create_path_for_image(i)]
    return images_paths


def main():
    # fun()
    # generate()

    images_paths = generate_images_paths(16)

    img = Image.open(images_paths[0])
    x = 100
    y = 500
    k = 9
    fragment = generate_fragment(img, x, y, k)
    generate_parameters_for_knn(fragment, k)


main()
