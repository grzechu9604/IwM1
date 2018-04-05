import matplotlib
import matplotlib.pyplot as plt
from skimage import data, io, feature, morphology
from skimage.morphology import square
from scipy import ndimage
import numpy

def filtr(obrazy):
	for id, img in enumerate(obrazy):
		dx = ndimage.sobel(img[1], 1)
		dy = ndimage.sobel(img[1], 0)	
		mag = numpy.hypot(dx, dy)
		mag *= 255.0 / numpy.max(mag)
		obrazy[id][1] = mag

def pokaz(obrazy):
	plt.figure(figsize=(8,8))
	
	for id, img in enumerate(obrazy):
		plt.subplot(1, 2, 1)
		plt.axis('off')
		io.imshow(img[0])

		plt.subplot(1, 2, 2)
		plt.axis('off')
		io.imshow(img[1])
	plt.show()

def main():
	image = ['healthy/01_h.jpg']
	obrazy = []
	for i in image:
		obrazy.append([data.imread(i, as_grey=False), data.imread(i, as_grey=True)])
	filtr(obrazy)
	pokaz(obrazy)
main()