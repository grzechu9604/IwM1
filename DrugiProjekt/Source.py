#!/usr/bin/env python
import matplotlib
import matplotlib.pyplot as plt
from skimage import data, io, feature
import numpy
import scipy
import cv2
from scipy import ndimage
from PIL import Image

def generate():

	im = Image.open('healthy/01_h.jpg')
	pix = im.load()
	h, w = im.size

	for y in range(1,h-1):
		for x in range(1,w-1):
			value_max = max(pix[y-1,x-1][0],pix[y-1,x][0],pix[y-1,x+1][0],pix[y,x-1][0],pix[y,x][0],pix[y,x+1][0],pix[y+1,x-1][0],pix[y+1,x][0],pix[y+1,x+1][0])
			if(value_max == pix[y,x][0]):
				pix[y,x]= (255,255,255)
	im.save("01_h.png")

def fun():
	plt.figure(figsize=(16,16))
	image = ['healthy/01_h.jpg']
	obrazy = []
	for i in image:
		plt.subplot(1, 2, 1)
		plt.axis('off')
		io.imshow(data.imread(i, as_grey=False))

		plt.subplot(1, 2, 2)
		plt.axis('off')
		grey = data.imread(i, as_grey=True)
		dx = ndimage.sobel(grey, 1)
		dy = ndimage.sobel(grey, 0)
		mag = numpy.hypot(dx, dy)
		mag *= 255.0 / numpy.max(mag)
		kernel = numpy.ones((5,5),numpy.uint8)
		erosion = cv2.erode(mag,kernel,iterations = 1)
		dilation = cv2.dilate(erosion,kernel,iterations = 1)
		io.imshow(dilation)
		plt.show()

def main():
	#fun()
	generate()

main()
