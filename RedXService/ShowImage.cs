OpenFileDialog open = new OpenFileDialog();
            string path = "";
            if (open.ShowDialog() == DialogResult.OK)
                path = open.FileName;
            if (path.Length < 1)
                return;
            Bitmap img1 = new Bitmap(Image.FromFile(path), new Size(50, 50));
            Bitmap img2 = new Bitmap(Image.FromFile(path), new Size(50, 50));
            Bitmap img3 = new Bitmap(Image.FromFile(path), new Size(50, 50));
            Bitmap img4 = new Bitmap(Image.FromFile(path), new Size(50, 50));

            this.pictureBox1.Image = img1;

            img2.RotateFlip(RotateFlipType.Rotate90FlipNone);
            this.pictureBox2.Image = img2;


            img3.RotateFlip(RotateFlipType.Rotate180FlipNone);
            this.pictureBox4.Image = img3;

            img4.RotateFlip(RotateFlipType.Rotate270FlipNone);
            this.pictureBox3.Image = img4;
